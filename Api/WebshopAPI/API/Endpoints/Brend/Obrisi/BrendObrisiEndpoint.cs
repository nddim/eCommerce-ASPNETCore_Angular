using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.Brend.Obrisi;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Brend.Obrisi
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("brend-obrisi")]
    public class BrendObrisiEndpoint : MyBaseEndpoint<BrendObrisiRequest, BrendObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrendObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ActionResult<BrendObrisiResponse>> Obradi([FromQuery] BrendObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var obj = await _applicationDbContext.Brend.FindAsync(request.BrendID);
            if (obj == null)
            {
                return NotFound($"Brend sa Id = {request.BrendID} ne postoji u bazi.");
            }

            _applicationDbContext.Remove(obj);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new BrendObrisiResponse
            {

            });

        }
    }
}
