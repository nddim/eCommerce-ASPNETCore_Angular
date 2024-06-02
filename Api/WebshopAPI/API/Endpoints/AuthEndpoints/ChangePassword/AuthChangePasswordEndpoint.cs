using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.ChangePassword
{
    [Authorize(Roles ="Admin, Kupac")]
    [Route("auth")]
    public class AuthChangePasswordEndpoint:MyBaseEndpoint<AuthChangePasswordRequest, AuthChangePasswordResponse>
    {
        private readonly IJwtHeaderService _myAuthService;
        private readonly UserManager<Korisnik> userManager;

        public AuthChangePasswordEndpoint( 
            IJwtHeaderService myAuthService,
            UserManager<Korisnik> userManager)
        {
            _myAuthService = myAuthService;
            this.userManager = userManager;
        }
        [HttpPost("change-password")]
        public override async Task<ActionResult<AuthChangePasswordResponse>> Obradi([FromBody]AuthChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = await _myAuthService.GetUser();

            if(korisnik==null) 
                return BadRequest("Korisnik nije prijavljen ili pogrešan email!");

            var sifraCheck = await userManager.CheckPasswordAsync(korisnik, request.StaraLozinka);

            if(!sifraCheck)
                return BadRequest("Pogrešna stara lozinka");

            if (request.NovaLozinka1 != request.NovaLozinka2 || request.NovaLozinka1 == "" || request.NovaLozinka1.Length <= 3 || request.NovaLozinka2.Length >= 50 ||
                request.NovaLozinka1 == null ||
                request.NovaLozinka2 == "" || request.NovaLozinka2.Length <= 3 || request.NovaLozinka2.Length >= 50 ||
                request.NovaLozinka2 == null)
            {
                return BadRequest("Problemi sa novom lozinkom");
            }

            await userManager.ChangePasswordAsync(korisnik, request.StaraLozinka, request.NovaLozinka1);      

            return Ok();
        }
    }
}
