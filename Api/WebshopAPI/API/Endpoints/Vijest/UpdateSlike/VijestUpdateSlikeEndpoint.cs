using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.UpdateSlike
{
    [Authorize(Roles = "Admin")]
    [Route("vijesti")]
    public class VijestUpdateSlikeEndpoint:MyBaseEndpoint<VijestUpdateSlikeRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VijestUpdateSlikeEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("slika-update")]
        public override async Task<ActionResult<NoResponse>> Obradi(VijestUpdateSlikeRequest request, CancellationToken cancellationToken = default)
        {
            var vijest = await _applicationDbContext.Vijest.FindAsync(request.Id, cancellationToken);
            if (vijest == null)
            {
                return BadRequest($"Nema proizvoda sa ID = {request.Id}");
            }
            var url = vijest.SlikaUrl;

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

            vijest.SlikaUrl = fileName;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }
}
