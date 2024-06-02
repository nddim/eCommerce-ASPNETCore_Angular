using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.Dodaj
{
    [Authorize(Roles = "Admin")]
    [Route("vijesti")]
    public class DodajVijestEndpoint:MyBaseEndpoint<DodajVijestRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public DodajVijestEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpPost("dodaj")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]DodajVijestRequest request, CancellationToken cancellationToken = default)
        {
            
            var noviObj = new Data.Models.Vijest()
            {
                Naziv = request.Naziv,
                Tekst = request.Tekst,
                Autor = "Administrator",
                Datum = DateTime.Now,
                BrojKlikova = 0
            };

            if (request.Slika != null)
            {
                string projekatFolder1 = Environment.CurrentDirectory;

                byte[] byteArray = Convert.FromBase64String(request.Slika.Split(',')[1]);

                string fileName = Guid.NewGuid().ToString() + "." + GetImageTypeFromBase64(request.Slika);
                string envFile = _environment.WebRootPath + "\\Uploads\\Images\\" + fileName;

                await System.IO.File.WriteAllBytesAsync(envFile, byteArray);

                noviObj.SlikaUrl = fileName;
            }
            await _applicationDbContext.Vijest.AddAsync(noviObj, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
        static string GetImageTypeFromBase64(string base64String)
        {
            var regex = new Regex(@"^data:image/(?<type>[a-zA-Z]+);base64,", RegexOptions.Compiled);

            var match = regex.Match(base64String);

            if (match.Success)
            {
                return match.Groups["type"].Value.ToLower();
            }

            return null;
        }
    }
}
