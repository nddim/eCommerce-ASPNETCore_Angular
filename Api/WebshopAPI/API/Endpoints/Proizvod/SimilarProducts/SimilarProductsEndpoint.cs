using Google.Apis.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.SimilarProducts
{
    [Route("similarproducts")]
    public class SimilarProductsEndpoint:MyBaseEndpoint<SimilarProductsRequest, SimilarProductsResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public SimilarProductsEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, IMemoryCache memoryCache)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<SimilarProductsResponse>> Obradi([FromQuery]SimilarProductsRequest request, CancellationToken cancellationToken = default)
        {
            var proizvod = await _applicationDbContext
                .Proizvod
                .Include(x => x.Potkategorija)
                .Include(x => x.Brend)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var proizvodiQuery = await _applicationDbContext
                .Proizvod
                .Include(x => x.Potkategorija)
                .Include(x => x.Brend)
                .Where(x=>x.Id!=request.Id)
                .Select(x => new SimilarProductsResponse
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
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
                    Slicnost = GetSimilarity(proizvod, x)
                }).ToListAsync(cancellationToken);

            var proizvodi = proizvodiQuery
                .OrderByDescending(x => x.Slicnost)
                .Take(9);

            return Ok(proizvodi);
        }

        private static int GetSimilarity(Data.Models.Proizvod? proizvod, Data.Models.Proizvod x)
        {
            if (proizvod == null)
                return 0;

            var brojac = 0;
            if (proizvod.BrendID == x.BrendID)
                brojac++;
            if(proizvod.PotkategorijaID==x.PotkategorijaID)
                brojac++;

            string[] proizvodNaziv = proizvod.Naziv.Split(' ');
            string[] xNaziv = x.Naziv.Split(' ');

            int brojIstihRijeci=proizvodNaziv.Intersect(xNaziv).Count();
            brojac += brojIstihRijeci;
            return brojac;
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
