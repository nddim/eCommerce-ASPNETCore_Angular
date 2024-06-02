using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using WebAPI.Endpoints.Proizvod.PretragaNovi;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaPopularni
{
    [Route("proizvod-popularni-pretraga")]
    public class ProizvodPopularniPretragaEndpoint : MyBaseEndpoint<ProizvodPopularniPretragaRequest, ProizvodPopularniPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        

        public ProizvodPopularniPretragaEndpoint(ApplicationDbContext applicationDbContext, 
            IWebHostEnvironment webHostEnvironment,
            IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _environment = webHostEnvironment;
            _memoryCache = memoryCache;
            
        }
        private readonly string cacheKey = "productsPopularniPretraga";

        [HttpGet]
        public override async Task<ActionResult<ProizvodPopularniPretragaResponse>> Obradi([FromQuery] ProizvodPopularniPretragaRequest request, CancellationToken cancellationToken = default)
        { 
            if (_memoryCache.TryGetValue(cacheKey, out List<ProizvodPopularniPretragaResponse> data))
            {

            }
            else
            {
                var datum = DateTime.Now.AddDays(30);
                data = await _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
                (request.PotkategorijaID == null || request.PotkategorijaID == 0 || request.PotkategorijaID == x.PotkategorijaID) &&
                (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .Include(x=>x.Klikovi)
                .Select(x => new ProizvodPopularniPretragaResponse
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    PocetnaCijena = x.PocetnaCijena,
                    PocetnaKolicina = x.PocetnaKolicina,
                    Opis = x.Opis,
                    BrojKlikova = x.Klikovi.Count(k=>datum>k.Datum), //GetBrojKlikova(x.Id, _applicationDbContext), //x.BrojKlikova,
                    PotkategorijaNaziv = x.Potkategorija.Naziv,
                    PotkategorijaID = x.PotkategorijaID,
                    BrendNaziv = x.Brend.Naziv,
                    BrendID = x.BrendID,
                    Popust = x.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment)
                })
                .OrderByDescending(x => x.BrojKlikova)
                .Take(8)
                .ToListAsync(cancellationToken: cancellationToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(300))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                   .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(cacheKey, data, cacheEntryOptions);
            }          

            return Ok(data);
        }
       

        private static int GetBrojKlikova(int id, ApplicationDbContext db)
        {
            var datum = DateTime.Now.AddDays(30);
            var brojKlikova = db.ProizvodKlik
                .Where(x => x.ProizvodId == id && datum > x.Datum)
                .Count();
            return brojKlikova;
        }
      
    }
}
