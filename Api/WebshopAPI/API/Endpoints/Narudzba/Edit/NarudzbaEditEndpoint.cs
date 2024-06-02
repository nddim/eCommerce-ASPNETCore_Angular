using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.SignalR;
using WebAPI.Services.JwtHeader;
using WebAPI.Services.Sms;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.Edit
{
    [Authorize(Roles ="Admin")]
    [Route("narudzba")]

    public class NarudzbaEditEndpoint:MyBaseEndpoint<NarudzbaEditRequest, NoResponse>
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _authService;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly ISMSService _smsService;
        private readonly UserManager<Korisnik> _userManager;

        public NarudzbaEditEndpoint(ApplicationDbContext applicationDbContext, IHubContext<SignalRHub> hubContext, IJwtHeaderService authService, ISMSService smsService, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
            _authService = authService;
            _smsService = smsService;
            _userManager = userManager;
        }
        [HttpPost("edit")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]NarudzbaEditRequest request, CancellationToken cancellationToken = default) 
        {
            var narudzba = await _applicationDbContext.Narudzba.Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (narudzba == null)
            {
                return NotFound("Narudzba ne postoji");
            }

            if (narudzba.StatusNarudzbeId == request.StatusNarudzbeId)
            {
                return BadRequest("Status vec postoji");
            }

            var potvrdena = _applicationDbContext.StatusNarudzbe.Where(x => x.Status.ToLower() == "potvrdena").FirstOrDefault();
            if (potvrdena!=null && request.StatusNarudzbeId==potvrdena.Id) // statusnarudzbe za potvrdu je 2 
            {
                narudzba.DatumPotvrde=DateTime.Now;
            }
            var poslato = _applicationDbContext.StatusNarudzbe.Where(x => x.Status.ToLower() == "poslato").FirstOrDefault();
            var userId = narudzba.KupacId;

            if (poslato!=null && request.StatusNarudzbeId==poslato.Id) // statusnarudzbe za poslano je 4 
            {
                narudzba.DatumSlanja=DateTime.Now;
                var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);

                await _smsService.PosaljiSMSPotvrdaNarudzbe(user.PhoneNumber, user.Ime + " " + user.Prezime);
            }
            
            narudzba.StatusNarudzbeId = request.StatusNarudzbeId;

            await _hubContext.Clients.Groups(userId).SendAsync("narudzba-posalji-notifikaciju", "!", cancellationToken);

         

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
