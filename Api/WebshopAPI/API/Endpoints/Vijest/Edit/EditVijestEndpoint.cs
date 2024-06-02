using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.Edit
{
    [Authorize(Roles = "Admin")]
    [Route("vijesti")]
    public class EditVijestEndpoint:MyBaseEndpoint<EditVijestRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public EditVijestEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpPost("edit")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]EditVijestRequest request, CancellationToken cancellationToken = default)
        {
            var vijest = await _applicationDbContext.Vijest.FindAsync(request.Id, cancellationToken);
            if (vijest == null)
            {
                return BadRequest($"Nema proizvoda sa ID = {request.Id}");
            }

            vijest.Naziv = request.Naziv;
            vijest.Tekst = request.Tekst;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
