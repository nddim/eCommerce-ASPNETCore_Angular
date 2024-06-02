using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Endpoints.Proizvod.PretragaPopularni;
using WebAPI.Helpers;
using WebshopApi.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Endpoints.Potkategorija.PocetnaPotkategorije
{
    [Route("potkategorije")]
    public class PocetnaPotkategorijeEndpoint : MyBaseEndpoint<NoRequest, PocetnaPotkategorijeResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMemoryCache _memoryCache;

        public PocetnaPotkategorijeEndpoint(ApplicationDbContext applicationDbContext, IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _memoryCache = memoryCache;
        }
        private readonly string cacheKey = "potkategorijePocetna";

        [HttpGet("pocetna")]
        public override async Task<ActionResult<PocetnaPotkategorijeResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            if (_memoryCache.TryGetValue(cacheKey, out List<PocetnaPotkategorijeResponse> lista))
            {

            }
            else
            {
                var potkategorije = await _applicationDbContext.Proizvod
                .Include(p => p.Potkategorija)
                .GroupBy(p => p.PotkategorijaID)
                .Select(g => new
                {
                    Id = g.Key,
                    Naziv = g.FirstOrDefault().Potkategorija.Naziv,
                    SumaKlikova = g.Sum(p => p.BrojKlikova)
                })
                .OrderByDescending(g => g.SumaKlikova)
                .Take(5)
                .ToListAsync(cancellationToken);

                lista = new List<PocetnaPotkategorijeResponse>();

                foreach (var p in potkategorije)
                    lista.Add(new PocetnaPotkategorijeResponse { Id = p.Id, Naziv = p.Naziv });

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(300))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(cacheKey, lista, cacheEntryOptions);
            }
            

            return Ok(lista);

        }
    }
}
