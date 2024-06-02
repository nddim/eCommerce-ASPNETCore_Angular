using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Endpoints.Proizvod.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaNovi
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-novi-pretraga")]
    public class ProizvodNoviPretragaEndpoint : MyBaseEndpoint<ProizvodNoviPretragaRequest, ProizvodNoviPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;

        public ProizvodNoviPretragaEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        private readonly string cacheKey = "productsNoviPretraga";

        [HttpGet]
        public override async Task<ActionResult<ProizvodNoviPretragaResponse>> Obradi([FromQuery] ProizvodNoviPretragaRequest request, CancellationToken cancellationToken = default)
        {
            if(_memoryCache.TryGetValue(cacheKey, out List<ProizvodNoviPretragaResponse> data))
            {

            }
            else
            {
                data = await _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
                (request.PotkategorijaID == null || request.PotkategorijaID == 0 || request.PotkategorijaID == x.PotkategorijaID) &&
                (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .OrderByDescending(x => x.Datum)
                .Select(x => new ProizvodNoviPretragaResponse
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    PocetnaCijena = x.PocetnaCijena,
                    PocetnaKolicina = x.PocetnaKolicina,
                    Opis = x.Opis,
                    BrojKlikova = x.BrojKlikova,
                    PotkategorijaNaziv = x.Potkategorija.Naziv,
                    PotkategorijaID = x.PotkategorijaID,
                    BrendNaziv = x.Brend.Naziv,
                    BrendID = x.BrendID,
                    Datum = x.Datum,
                    Popust = x.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment)

                }).ToListAsync(cancellationToken: cancellationToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(cacheKey, data, cacheEntryOptions);

            }
                           

            return Ok(data);
        }
        private static string GetImageByProduct(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string HostUrl = "https://localhost:7110";
            string filepath = GetFilePath(path, env);
            if (!System.IO.File.Exists(filepath))
            {
                imageUrl = HostUrl + "/Uploads/Images/" + "NoImage.png";
            }
            else
            {
                imageUrl = HostUrl + "/Uploads/Images/" + path;
            }

            return imageUrl;
        }

        private static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
        }


    }
}
