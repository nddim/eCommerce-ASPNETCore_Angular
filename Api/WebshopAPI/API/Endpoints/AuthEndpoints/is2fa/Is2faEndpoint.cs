using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.is2fa
{
    [Authorize]
    [Route("auth/is2fa")]
    public class Is2faEndpoint : MyBaseEndpoint<NoRequest, Is2faResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public Is2faEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpGet()]
        public override async Task<ActionResult<Is2faResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var is2fa = await _myAuthService.Is2fa();

            if (is2fa)
            {
                return Ok();
            }
            return StatusCode(210);
        }
    }
}
