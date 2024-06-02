using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Endpoints.Brend.Update;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Brend.Update
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("brend-update")]
    public class BrendUpdateEndpoint : MyBaseEndpoint<BrendUpdateRequest, BrendUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BrendUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<BrendUpdateResponse>> Obradi([FromBody]BrendUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var brend = await _applicationDbContext.Brend.FindAsync(request.Id);

            if (brend == null || request.Naziv.IsNullOrEmpty() || string.IsNullOrWhiteSpace(request.Naziv))
            {
                return NotFound($"Nema brend sa Id = {request.Id} u bazi ili je brend prazan string!");
            }
            brend.Naziv = request.Naziv;
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new BrendUpdateResponse()
            {
                Id = request.Id,
                Naziv = request.Naziv
            });

        }
    }
}
