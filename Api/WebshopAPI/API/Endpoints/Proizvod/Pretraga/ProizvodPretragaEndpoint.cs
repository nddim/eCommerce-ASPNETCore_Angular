using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.Pretraga
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-pretraga")]
    public class ProizvodPretragaEndpoint : MyBaseEndpoint<ProizvodPretragaRequest, ProizvodPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;


        public ProizvodPretragaEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaResponse>> Obradi([FromQuery] ProizvodPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().Contains(request.Naziv.ToLower())) &&
                (request.PotkategorijaID == null || request.PotkategorijaID == 0 || request.PotkategorijaID == x.PotkategorijaID) &&
                (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .Select(x => new ProizvodPretragaResponseObject
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
                    Popust = x.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
                    Datum=x.Datum
                });

            switch (request.SortID)
            {
                case 2:
                    query = query.OrderBy(x => x.PocetnaCijena);
                    break;
                case 3:
                    query = query.OrderByDescending(x => x.PocetnaCijena);
                    break;
                case 4:
                    query = query.OrderBy(x => x.Naziv);
                    break;
                case 5:
                    query = query.OrderByDescending(x => x.PocetnaCijena);
                    break;
                case 6:
                    query = query.OrderByDescending(x => x.Datum);
                    break;
                case 7:
                    query = query.OrderBy(x => x.Datum);
                    break;
            }

            if (query.Count() > 0 && request.PageSize != 0)
            {
                var najmanjaCijena = query.Min(x => x.PocetnaCijena);
                var najvecaCijena = query.Max(x => x.PocetnaCijena);


                var paged = PagedList<ProizvodPretragaResponseObject>.Create(query, request.PageNumber, request.PageSize);

                return Ok(new ProizvodPretragaResponse()
                {
                    Proizvodi = paged.DataItems,
                    min = najmanjaCijena, //proizvodi.OrderBy(x => x.PocetnaCijena).First().PocetnaCijena,
                    max = najvecaCijena, //proizvodi.OrderByDescending(x => x.PocetnaCijena).First().PocetnaCijena,
                    PageSize = paged.PageSize,
                    CurrentPage = paged.CurrentPage,
                    TotalCount = paged.TotalCount,
                    TotalPages = paged.TotalPages
                });
            }

            return Ok(new ProizvodPretragaResponse()
            {
                Proizvodi = query.ToList(),
                min = 0,
                max = 0,
            });
        }

        private static string GetImageByProduct(string path, IWebHostEnvironment env)
        {
            string imageUrl=string.Empty;
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
