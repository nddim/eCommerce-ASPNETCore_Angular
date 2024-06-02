using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korpa.UpdateKolicina
{
    [Authorize(Roles = "Kupac")]
    [Route("korpa")]
    public class KorpaUpdateEndpoint:MyBaseEndpoint<KorpaUpdateRequest,NoResponse>
    {
        private ApplicationDbContext _applicationDbContext;
        private IJwtHeaderService _myAuthService;

        public KorpaUpdateEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPost("update-kolicina")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody] KorpaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var pronadjiProizvod = await _applicationDbContext.Korpa.Where(x => x.KupacId==user.Id && x.ProizvodId == request.ProizvodId)
                .FirstOrDefaultAsync(cancellationToken);
            var proizvod = await _applicationDbContext.Proizvod.Where(x => x.Id == request.ProizvodId).FirstOrDefaultAsync(cancellationToken);
            if (pronadjiProizvod != null)
            {
                if (proizvod != null)
                {
                    if (proizvod.PocetnaKolicina >= request.Kolicina)
                    {
                        pronadjiProizvod.Kolicina = request.Kolicina;
                        await _applicationDbContext.SaveChangesAsync(cancellationToken);
                        return Ok();
                    }
                    return BadRequest("Prevelika količina, nemamo na lageru. Moguće dodati u korpu maksimalno: "+proizvod.PocetnaKolicina.ToString()+" proizvoda");
                }
                return BadRequest("Pogrešan proizvod!");
            }
            else
            {
                return NotFound("Proizvod nije pronaden");
            }
        }
    }
}
