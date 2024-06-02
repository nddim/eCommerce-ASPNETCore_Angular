using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.GetById
{
    [Route("proizvod")]
    public class ProizvodGetByIdEndpoint:MyBaseEndpoint<ProizvodGetByIdRequest, ProizvodGetByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;


        public ProizvodGetByIdEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet("getbyid")]
        public override async Task<ActionResult<ProizvodGetByIdResponse>> Obradi([FromQuery]ProizvodGetByIdRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Proizvod
                .FindAsync(request.Id);

            if (data != null)
            {
                var slike = await _applicationDbContext
                    .ProizvodSlika
                    .Where(x => x.ProizvodId == request.Id)
                    .ToListAsync(cancellationToken);

                var slikeGalerija=new List<SlikaDTO>();

                foreach (var slika in slike) {
                    var slikaUrl = FajloviHelper.GetImageByProduct(slika.SlikaUrl, _environment);
                    slikeGalerija.Add(new SlikaDTO { Id=slika.Id, SlikaUrl=slikaUrl});
                }

                return Ok(new ProizvodGetByIdResponse()
                {
                    Id = data.Id,
                    Naziv = data.Naziv,
                    PocetnaCijena = data.PocetnaCijena,
                    PocetnaKolicina = data.PocetnaKolicina,
                    Opis = data.Opis,
                    BrojKlikova = data.BrojKlikova,
                   // PotkategorijaNaziv = data.Potkategorija.Naziv,
                    PotkategorijaID = data.PotkategorijaID,
                   // BrendNaziv = data.Brend.Naziv,
                    BrendID = data.BrendID,
                    Popust = data.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(data.SlikaUrl, _environment),
                    SlikeGalerija= slikeGalerija,
                });
            }

            return Ok("Wrong id");
        }

        private static string GetImageByProduct(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string HostUrl = "https://localhost:7110";
            string filepath = GetFilePath(path, env);
            if (!System.IO.File.Exists(filepath))
            {
                imageUrl = HostUrl + "/Uploads/Images/NoImage.png";
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
