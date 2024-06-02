using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Proizvod.PretragaCijenaMaxPopust;
using WebAPI.Endpoints.Proizvod.PretragaCijenaMin;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaCijenaMinPopust
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-cijenamin-popust-pretraga")]
    public class ProizvodCijenaMinPopustEndpoint : MyBaseEndpoint<ProizvodCijenaMinPopustRequest, ProizvodCijenaMinPopustResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public ProizvodCijenaMinPopustEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodCijenaMinPopustResponse>> Obradi([FromQuery] ProizvodCijenaMinPopustRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Proizvod
                .Where(x => x.Popust > 0 && (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())))
                .OrderBy(x => x.PocetnaCijena)
                .Select(x => new ProizvodCijenaMinPopustResponse
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
