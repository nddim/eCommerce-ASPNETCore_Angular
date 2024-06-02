using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Endpoints.Proizvod.PretragaByNaziv;
using WebAPI.Helpers;
using WebshopApi.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Proizvod.PretragaByBrends
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-pretraga-all")]
    public class ProizvodPretragaByBrendEndpoint:MyBaseEndpoint<ProizvodPretragaByBrendRequest, ProizvodPretragaByBrendResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;

        public ProizvodPretragaByBrendEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async override Task<ActionResult<ProizvodPretragaByBrendResponse>> Obradi([FromQuery]ProizvodPretragaByBrendRequest request, CancellationToken cancellationToken = default)
        {
            string cacheKey = CacheKey.ProductsByBrendsGenerator(request);

            if(_memoryCache.TryGetValue(cacheKey, out ProizvodPretragaByBrendResponse data))
            {
                return Ok(data);
            }
            else
            {
                var query = _applicationDbContext
                .Proizvod
                .Where(x => (x.PotkategorijaID == request.PotkategorijaID) &&
                            (request.Brendovi == null || request.Brendovi.Contains(x.BrendID)) &&
                            ((request.Min == 0 && request.Max == 0) || // Ako nema zadanih granica, sve cijene su prihvatljive
                            ((x.Popust > 0 && (x.Popust >= request.Min && x.Popust <= request.Max)) || // Ako ima popust, uzimamo u obzir popust kao pravu cijenu
                            ((x.Popust == 0 && x.PocetnaCijena >= request.Min && x.PocetnaCijena <= request.Max) // Ako nema popusta, koristimo pocetnaCijena
                            ))))
                .Select(x => new ProizvodPretragaByBrendObject()
                {
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
                    Naziv = x.Naziv,
                    Id = x.Id,
                    PocetnaCijena = x.PocetnaCijena,
                    Opis = x.Opis,
                    Popust = x.Popust,
                    DatumDodavanja = x.Datum
                });

                var queryCopy = _applicationDbContext
                    .Proizvod
                    .Where(x => (x.PotkategorijaID == request.PotkategorijaID))
                    .Select(x => new
                    {
                        Id = x.Id,
                        PocetnaCijena = x.PocetnaCijena,
                        Popust = x.Popust
                    });

                switch (request.SortID)
                {
                    case 2:
                        query = query.OrderBy(x => x.Popust != 0 ? x.Popust : x.PocetnaCijena);
                        break;
                    case 3:
                        query = query.OrderByDescending(x => x.Popust != 0 ? x.Popust : x.PocetnaCijena);
                        break;
                    case 4:
                        query = query.OrderBy(x => x.Naziv);
                        break;
                    case 5:
                        query = query.OrderByDescending(x => x.Naziv);
                        break;
                    case 6:
                        query = query.OrderBy(x => x.DatumDodavanja);
                        break;
                    case 7:
                        query = query.OrderByDescending(x => x.DatumDodavanja);
                        break;
                }

                var minCijena = 0f;
                var najmanjaCijena = 0f;
                var najmanjaCijenaPopustKolicina = 0;
                if (queryCopy.Count() > 0)
                {
                    najmanjaCijena = queryCopy.Min(x => x.PocetnaCijena);
                    najmanjaCijenaPopustKolicina = queryCopy.Where(x => x.Popust > 0).Count();
                }
                               
                var najmanjaCijenaPopust = 0f;

                if (najmanjaCijenaPopustKolicina > 0)
                {
                    najmanjaCijenaPopust = queryCopy.Where(x => x.Popust > 0).Min(x => x.Popust);
                }
                if (najmanjaCijenaPopust > 0 && najmanjaCijenaPopust < najmanjaCijena)
                    najmanjaCijena = najmanjaCijenaPopust;

                var najvecaCijena = 0f;
                if (queryCopy.Count() > 0)
                {
                    najvecaCijena = queryCopy.Max(x => x.PocetnaCijena);
                }


                if (query.Count() > 0 && request.PageSize != 0)
                {
                    var paged = PagedList<ProizvodPretragaByBrendObject>.Create(query, request.PageNumber, request.PageSize);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(120))
                    .SetPriority(CacheItemPriority.Normal);

                    var dat = new ProizvodPretragaByBrendResponse()
                    {
                        Proizvodi = paged.DataItems,
                        min = najmanjaCijena, //proizvodi.OrderBy(x => x.PocetnaCijena).First().PocetnaCijena,
                        max = najvecaCijena, //proizvodi.OrderByDescending(x => x.PocetnaCijena).First().PocetnaCijena,
                        PageSize = paged.PageSize,
                        CurrentPage = paged.CurrentPage,
                        TotalCount = paged.TotalCount,
                        TotalPages = paged.TotalPages
                    };
                    _memoryCache.Set(cacheKey, dat, cacheEntryOptions);

                    return Ok(dat);
                }

                var cacheEntryOptionss = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(120))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(120))
                   .SetPriority(CacheItemPriority.Normal);

                var datt = new ProizvodPretragaByBrendResponse()
                {
                    Proizvodi = query.ToList(),
                    min = najmanjaCijena,
                    max = najvecaCijena
                };

                _memoryCache.Set(cacheKey, datt, cacheEntryOptionss);

                return Ok(datt);
            }
        }
    }
}
