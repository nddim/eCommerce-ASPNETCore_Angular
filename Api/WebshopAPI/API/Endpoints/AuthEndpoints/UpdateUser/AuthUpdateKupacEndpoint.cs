using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.UpdateUser
{
    [Authorize()]
    [Route("auth")]
    public class AuthUpdateKupacEndpoint:MyBaseEndpoint<AuthUpdateKupacRequest, AuthUpdateKupacResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public AuthUpdateKupacEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost("update-kupac")]
        public override async Task<ActionResult<AuthUpdateKupacResponse>> Obradi([FromBody]AuthUpdateKupacRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = await _myAuthService.GetUser();

            if (korisnik == null)
            {
                return BadRequest(new NoResponse());
            }
            if (korisnik.Email != request.Email)
                return BadRequest("Različiti mailovi!");

            if (request == null || request.Ime == null || request.Ime.Length < 3 || request.Ime.Length > 50
                || request.Prezime == null || request.Prezime.Length < 3 || request.Prezime.Length > 50
                || request.Email == null || request.Email.Length < 3 || request.Email.Length > 50)
            {
                return BadRequest(new NoResponse());
            }

            korisnik.Ime=request.Ime;  
            korisnik.Prezime=request.Prezime;
            korisnik.Email=request.Email;
            korisnik.Adresa=request.Adresa;
            korisnik.PhoneNumber=request.BrojTelefona;
            korisnik.SaljiNovosti=request.SaljiNovosti;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new AuthUpdateKupacResponse()
            {
                Email = request.Email,
                Adresa = request.Adresa,
                BrojTelefona = request.BrojTelefona,
                Ime = request.Ime,
                Prezime = request.Prezime,
                SaljiNovosti = request.SaljiNovosti
            });
        }
    }
}
