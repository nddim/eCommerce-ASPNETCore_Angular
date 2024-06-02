using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.ProizvodSlika.Dodaj
{
    [Authorize(Roles = "Admin")]
    [Route("proizvodslika")]
    public class ProizvodSlikaDodajEndpoint : MyBaseEndpoint<ProizvodSlikaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProizvodSlikaDodajEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost()]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]ProizvodSlikaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var proizvod=await _applicationDbContext.Proizvod.FindAsync(request.ProizvodId, cancellationToken);
            if (proizvod == null)
                return BadRequest("Pogrešan proizvod id");

            string projekatFolder1 = Environment.CurrentDirectory;

            byte[] byteArray = Convert.FromBase64String(request.Slika.Split(',')[1]);

            string fileName = Guid.NewGuid().ToString() + "." + FajloviHelper.GetImageTypeFromBase64(request.Slika);
            string envFile = _webHostEnvironment.WebRootPath + "\\Uploads\\Images\\" + fileName;

            await System.IO.File.WriteAllBytesAsync(envFile, byteArray);

            var proizvodSlika = new WebAPI.Data.Models.ProizvodSlika
            {
                ProizvodId = proizvod.Id,
                SlikaUrl = fileName,
            };
            await _applicationDbContext.AddAsync(proizvodSlika, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
