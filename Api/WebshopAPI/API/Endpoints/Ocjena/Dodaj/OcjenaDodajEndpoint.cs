using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Ocjena.Dodaj
{
    [Authorize(Roles ="Kupac")]
    [Microsoft.AspNetCore.Components.Route("ocjena-dodaj")]
    public class OcjenaDodajEndpoint:MyBaseEndpoint<OcjenaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public OcjenaDodajEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost("ocjena-dodaj")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody] OcjenaDodajRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();
            if (user == null)
            {
                return BadRequest("Niste prijavljeni");
            }

            var vecOcjenjen = await _applicationDbContext.Ocjena.Where(x => x.ProizvodId == request.ProizvodId && x.KupacId == user.Id).FirstOrDefaultAsync(cancellationToken);

            var kupljen = await _applicationDbContext
                .StavkeNarudzbe
                .Include(x => x.Narudzba)
                .Where(x => x.Narudzba.KupacId == user.Id && x.ProizvodId == request.ProizvodId)
                .FirstOrDefaultAsync(cancellationToken);

            if (kupljen == null)
            {
                return BadRequest("Niste kupili ovaj proizvod, tako da ne možete ocijeniti!");
            }

            if (vecOcjenjen == null)
            {
                var noviObj = new Data.Models.Ocjena
                {
                    KupacId = user.Id,
                    Vrijednost = request.Vrijednost,
                    ProizvodId = request.ProizvodId
                };

                _applicationDbContext.Ocjena.Add(noviObj);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                return Ok("Proizvod je vec ocjenjen");
            }
            return Ok(new NoResponse());

        }
    }
}
