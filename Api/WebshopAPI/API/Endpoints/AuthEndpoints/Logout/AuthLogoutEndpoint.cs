using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth.Loggeri.Interfacei;
using WebshopApi.Data;
using WebAPI.Services.JwtHeader;

namespace WebAPI.Endpoints.AuthEndpoints.Logout
{
    [Route("auth")]
    public class AuthLogoutEndpoint:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _authService;
        private readonly ILoggerOdjava _logger;

        public AuthLogoutEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService authService, ILoggerOdjava logger)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _logger = logger;
        }

        [NonAction]
        [HttpPost("logout")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody] NoRequest request, CancellationToken cancellationToken)
        {
            return Ok(new NoResponse());
        }
    }
}
