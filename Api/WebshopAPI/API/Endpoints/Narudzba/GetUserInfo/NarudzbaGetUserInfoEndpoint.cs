using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.GetUserInfo
{
    [Authorize]
    [Route("narudzba")]
    public class NarudzbaGetUserInfoEndpoint:MyBaseEndpoint<NoRequest, NarudzbaGetUserInfoResponse>
    {
        private readonly IJwtHeaderService _myAuthService;

        public NarudzbaGetUserInfoEndpoint(IJwtHeaderService myAuthService)
        {
            _myAuthService = myAuthService;
        }
        [HttpGet("get-userinfo")]
        public override async Task<ActionResult<NarudzbaGetUserInfoResponse>> Obradi([FromQuery] NoRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var korisnik = new NarudzbaGetUserInfoResponse()
            {
                Id = user.Id,
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email = user.Email,
                Adresa = user.Adresa,
                KontaktBroj = user.PhoneNumber
            };
            return Ok(korisnik);

        }
    }
}
