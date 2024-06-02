using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.RegisterActivation
{
    [Route("auth")]
    public class AuthRegisterActivateEndpoint:MyBaseEndpoint<RegisterActivateRequest, RegisterActivateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthRegisterActivateEndpoint(ApplicationDbContext applicationDbContext)//, MyEmailSenderService emailSenderService) //IHubContext<PorukeHub> hubContext
        {
            _applicationDbContext = applicationDbContext;
        }
        [NonAction]
        [HttpPost("activate")]
        public async override Task<ActionResult<RegisterActivateResponse>> Obradi([FromBody]RegisterActivateRequest request, CancellationToken cancellationToken = default)
        {
            var obj = await _applicationDbContext.RacunAktivacija.Where(x => x.ActivateKey == request.ActivateCode)
                .FirstOrDefaultAsync(cancellationToken);

            if (obj == null)
            {
                return BadRequest(new RegisterActivateResponse() { Uredu = false});
            }

            var korisnickiRacunId = obj.KorisnikId;
            var korisnik = await _applicationDbContext.Korisnik.Where(x => x.Id == korisnickiRacunId)
                .FirstOrDefaultAsync(cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new RegisterActivateResponse() { Uredu = true});

        }
    }
}
