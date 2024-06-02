using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Endpoints.Proizvod.Pretraga;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Endpoints.Proizvod.PretragaById
{
    //[Authorize(Roles ="Kupac")]
    [Microsoft.AspNetCore.Mvc.Route("proizvod-pretragaById")]
    public class ProizvodPretragaByIdEndpoint : MyBaseEndpoint<ProizvodPretragaByIdRequest, ProizvodPretragaByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IJwtHeaderService _jwtHeader;

        public ProizvodPretragaByIdEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, IJwtHeaderService jwtHeader)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            this._jwtHeader = jwtHeader;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaByIdResponse>> Obradi([FromQuery] ProizvodPretragaByIdRequest request, CancellationToken cancellationToken = default)
        {
            

            var proizvod = await _applicationDbContext.Proizvod
                .Include(x => x.Potkategorija).Include(x => x.Brend)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (proizvod == null)
                return NotFound("wrong id");
            if (request.View)
            {
                _applicationDbContext.ProizvodKlik.Add(new Data.Models.ProizvodKlik { ProizvodId = proizvod.Id, Datum = DateTime.Now });
                proizvod.BrojKlikova++;
                await _applicationDbContext.SaveChangesAsync();
            }

            var slike = await _applicationDbContext
                .ProizvodSlika
                .Where(x => x.ProizvodId == request.Id)
                .ToListAsync(cancellationToken);

            var slikeGalerija = new List<string>();
            var prvaSlika = FajloviHelper.GetImageByProduct(proizvod.SlikaUrl, _environment);
            if(prvaSlika!= null)
                slikeGalerija.Add(prvaSlika);
            foreach (var slika in slike)
            {
                var slikaUrl = FajloviHelper.GetImageByProduct(slika.SlikaUrl, _environment);
                slikeGalerija.Add(slikaUrl);
            }

            return Ok(new ProizvodPretragaByIdResponse
            {
                Id = proizvod.Id,
                Naziv = proizvod.Naziv,
                PocetnaCijena = proizvod.PocetnaCijena,
                PocetnaKolicina = proizvod.PocetnaKolicina,
                Opis = proizvod.Opis,
                BrojKlikova = proizvod.BrojKlikova,
                PotkategorijaNaziv = proizvod.Potkategorija.Naziv,
                PotkategorijaID = proizvod.PotkategorijaID,
                BrendNaziv = proizvod.Brend.Naziv,
                BrendID = proizvod.BrendID,
                SlikaUrl = FajloviHelper.GetImageByProduct(proizvod.SlikaUrl, _environment),
                Popust=proizvod.Popust,
                SlikeGalerija=slikeGalerija
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
