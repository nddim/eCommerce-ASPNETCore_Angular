using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAPI.Data.Models;
using WebAPI.Endpoints.Proizvod.Dodaj;
using WebAPI.Endpoints.Proizvod.UploadSlike;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.DodajSaSlikom
{
    [Authorize(Roles = "Admin")]
    [Route("proizvod")]
    public class ProizvodDodajKompletEndpoint:MyBaseEndpoint<ProizvodDodajKompletRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public ProizvodDodajKompletEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpPost("dodaj")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]ProizvodDodajKompletRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.Naziv == "")
            {
                return BadRequest("Nije unesen tekst potkategorije.");
            }
            var obj = new WebAPI.Data.Models.Proizvod()
            {
                Naziv = request.Naziv,
                PotkategorijaID = request.PotkategorijaID,
                BrendID = request.BrendID,
                PocetnaKolicina = request.PocetnaKolicina,
                PocetnaCijena = request.PocetnaCijena,
                Opis = request.Opis,
                Datum = DateTime.Now,
                Popust = request.Popust,
            };


            if (request.Slika != null)
            {
                string projekatFolder1 = Environment.CurrentDirectory;

                byte[] byteArray = Convert.FromBase64String(request.Slika.Split(',')[1]);             

                string fileName = Guid.NewGuid().ToString() + "." + GetImageTypeFromBase64(request.Slika);
                string envFile = _environment.WebRootPath + "\\Uploads\\Images\\" + fileName;

                await System.IO.File.WriteAllBytesAsync(envFile, byteArray);

                obj.SlikaUrl = fileName;
            }
            await _applicationDbContext.AddAsync(obj, cancellationToken);
            await _applicationDbContext.SaveChangesAsync();

            if (request.SlikeGalerija != null)
            {
                foreach (var slika in request.SlikeGalerija)
                {
                    string projekatFolder1 = Environment.CurrentDirectory;

                    byte[] byteArray = Convert.FromBase64String(slika.Split(',')[1]);

                    string fileName = Guid.NewGuid().ToString() + "." + GetImageTypeFromBase64(slika);
                    string envFile = _environment.WebRootPath + "\\Uploads\\Images\\" + fileName;

                    await System.IO.File.WriteAllBytesAsync(envFile, byteArray);

                    var proizvodSlika = new WebAPI.Data.Models.ProizvodSlika
                    {
                        ProizvodId = obj.Id,
                        SlikaUrl = fileName,
                    };
                    await _applicationDbContext.AddAsync(proizvodSlika, cancellationToken);
                }
            }

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new NoResponse());
        }

        static string GetImageTypeFromBase64(string base64String)
        {
            var regex = new Regex(@"^data:image/(?<type>[a-zA-Z]+);base64,", RegexOptions.Compiled);

            var match = regex.Match(base64String);

            if (match.Success)
            {
                return match.Groups["type"].Value.ToLower();
            }

            return "png";
        }

    }
}
