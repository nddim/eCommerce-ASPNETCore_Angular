using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.Delete
{
    [Authorize(Roles = "Admin")]
    [Route("vijest/delete")]
    public class VijestDeleteEndpoint:MyBaseEndpoint<int, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        public VijestDeleteEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpDelete("{id}")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var vijest = await _applicationDbContext.Vijest.FindAsync(id);
            if (vijest == null)
            {
                return NotFound("Vijest ne postoji");
            }

            _applicationDbContext.Remove(vijest);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
