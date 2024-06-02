using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.ProizvodSlika.Izbrisi
{
    [Authorize(Roles = "Admin")]
    [Route("proizvodslika")]
    public class ProizvodSlikaIzbrisiEndpoint : MyBaseEndpoint<ProizvodSlikaIzbrisiRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProizvodSlikaIzbrisiEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpDelete()]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]ProizvodSlikaIzbrisiRequest request, CancellationToken cancellationToken = default)
        {
            var proizvodSlika=await _applicationDbContext
                .ProizvodSlika
                .FindAsync(request.Id, cancellationToken);

            if (proizvodSlika == null)
                return BadRequest("Pogrešan proizvod slika id!");

            var url = proizvodSlika.SlikaUrl;

            string imageUrl = string.Empty;
            string filepath = ImageHelper.GetFilePath(url, _webHostEnvironment);
            if (!filepath.ToLower().Contains("noimage") && System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            _applicationDbContext.ProizvodSlika.Remove(proizvodSlika);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok(new NoResponse());
        }
    }
}
