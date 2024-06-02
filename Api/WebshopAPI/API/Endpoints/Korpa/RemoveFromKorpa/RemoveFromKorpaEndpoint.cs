using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korpa.RemoveFromKorpa
{
    [Authorize(Roles ="Kupac")]
    [Route("korpa")]
    public class RemoveFromKorpaEndpoint:MyBaseEndpoint<RemoveFromKorpaRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public RemoveFromKorpaEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpDelete("removefromcart")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]RemoveFromKorpaRequest request, CancellationToken cancellationToken = default)
        {
            var loggedUser = await _myAuthService.GetUser();

            if (loggedUser == null)
            {
                return BadRequest("user not logged in");
            }

            var artikal = await _applicationDbContext
                .Korpa
                .Where(x => x.Id == request.Id && x.KupacId == loggedUser.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (artikal == null)
                return BadRequest("wrong artikal id");

            _applicationDbContext.Remove(artikal);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
