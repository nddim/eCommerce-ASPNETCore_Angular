using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.UpdateSlike
{
    [Authorize(Roles = "Admin")]
    [Route("proizvod")]
    public class UpdateSlikeEndpoint:MyBaseEndpoint<UpdateSlikeRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateSlikeEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("slika-update")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]UpdateSlikeRequest request, CancellationToken cancellationToken = default)
        {
            var proizvod = await _applicationDbContext.Proizvod.FindAsync(request.Id);
            if (proizvod == null)
            {
                return BadRequest("wrong id");
            }

            var url = proizvod.SlikaUrl;

            string imageUrl = string.Empty;
            string filepath = ImageHelper.GetFilePath(url, _webHostEnvironment);
            if (!filepath.ToLower().Contains("noimage") && System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            byte[] byteArray = Convert.FromBase64String(request.Slika.Split(',')[1]);



            string fileName = Guid.NewGuid().ToString() + "." + ImageHelper.GetImageTypeFromBase64(request.Slika);
            string envFile = _webHostEnvironment.WebRootPath + "\\Uploads\\Images\\" + fileName;

            await System.IO.File.WriteAllBytesAsync(envFile, byteArray);
            proizvod.SlikaUrl = fileName;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new NoResponse());
        }
    }
}
