using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.TwoFOtkljucaj
{
    [Route("twofkey")]
    public class TwoFKeyAuthEndpoint : MyBaseEndpoint<TwoFKeyAuthRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public TwoFKeyAuthEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [NonAction]
        [HttpPost()]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]TwoFKeyAuthRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().korisnickiRacun;

            if (user == null)
            {
                return BadRequest("Nije prijavljen!");
            }

            var token=_myAuthService.GetAuthInfo().autentifikacijaToken;

            if (token == null)
            {
                return BadRequest("Nema prijave");
            }

           

            if (token.VrijemeValidnostiTwoFKey < DateTime.Now)
            {
                _applicationDbContext.AutentifikacijaToken.Remove(token);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return BadRequest("vrijeme");
            }
            if (request.Key != token.TwoFKey)
            {
                _applicationDbContext.AutentifikacijaToken.Remove(token);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return BadRequest("kljuc");
            }

            token.Is2FOtkljucano = true;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
