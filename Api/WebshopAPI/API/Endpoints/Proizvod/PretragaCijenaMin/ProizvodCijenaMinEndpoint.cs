using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Proizvod.PretragaNovi;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaCijenaMin
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-cijenamin-pretraga")]
    public class ProizvodCijenaMinEndpoint : MyBaseEndpoint<ProizvodCijenaMinRequest, ProizvodCijenaMinResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public ProizvodCijenaMinEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodCijenaMinResponse>> Obradi([FromQuery] ProizvodCijenaMinRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
                (request.PotkategorijaID == null || request.PotkategorijaID == 0 || request.PotkategorijaID == x.PotkategorijaID) &&
                (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .OrderBy(x => x.PocetnaCijena)
                .Select(x => new ProizvodCijenaMinResponse
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
