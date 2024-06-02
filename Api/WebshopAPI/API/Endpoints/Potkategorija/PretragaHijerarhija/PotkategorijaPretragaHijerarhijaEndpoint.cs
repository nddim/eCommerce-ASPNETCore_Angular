using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Endpoints.Potkategorija.PocetnaPotkategorije;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Potkategorija.PretragaHijerarhija
{
    [Route("kategorije")]
    public class PotkategorijaPretragaHijerarhijaEndpoint : MyBaseEndpoint<PotkategorijaPretragaHijerarhijaRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMemoryCache _memoryCache;

        public PotkategorijaPretragaHijerarhijaEndpoint(ApplicationDbContext applicationDbContext, IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _memoryCache = memoryCache;
        }
        private readonly string cacheKey = "potkategorijePretraga";

        [HttpGet("hijerarhija")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]PotkategorijaPretragaHijerarhijaRequest request, CancellationToken cancellationToken = default)
        {
            if (_memoryCache.TryGetValue(cacheKey, out PotkategorijaPretragaHijerarhijaResponse lista))
            {

            }
            else
            {
                var hijerarhija = await _applicationDbContext
                               .GlavnaKategorija
                               .Include(gk => gk.Kategorije)
                               .ThenInclude(k => k.Potkategorije)
                               .Select(x => new PotkategorijaPretragaHijerarhijaResponseObject
                               {
                                   Id = x.Id,
                                   Naziv = x.Naziv,
                                   Kategorije = x.Kategorije.Select(k => new KategorijaListDto
                                   {
                                       Id = k.Id,
                                       Naziv = k.Naziv,
                                       Potkategorije = k.Potkategorije.Select(p => new Potkategorija
                                       {
                                           Id = p.Id,
                                           Naziv = p.Naziv
                                       }).ToList()
                                   }).ToList()
                               })
                               .ToListAsync(cancellationToken);

                lista = new PotkategorijaPretragaHijerarhijaResponse() { GlavneKategorije = hijerarhija };

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
