using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korpa.Add
{
    [Authorize(Roles ="Kupac")]
    [Route("korpa")]
    public class AddKorpaArtikalEndpoint:MyBaseEndpoint<AddKorpaArtikalRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public AddKorpaArtikalEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost("dodaj")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]AddKorpaArtikalRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var vecPostoji = await _applicationDbContext.Korpa
                .Where(x => x.KupacId == user.Id && x.ProizvodId == request.ProizvodId)
                .FirstOrDefaultAsync(cancellationToken);

            if (vecPostoji == null)
            {
                var noviObj = new Data.Models.Korpa
                {
                    Kupac = user,
                    Kolicina = request.Kolicina,
                    ProizvodId = request.ProizvodId,
                };

                _applicationDbContext.Korpa.Add(noviObj);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var pr = _applicationDbContext.Proizvod.Where(x => x.Id == request.ProizvodId).FirstOrDefault();
                if(vecPostoji.Kolicina<pr.PocetnaKolicina)
                {
                    vecPostoji.Kolicina++;
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                    return Ok();
                }
                return BadRequest("Dodana maksimalna količina ovog proizvoda koja iznosi: " + pr.PocetnaKolicina);
                
            }
            var proizvod = _applicationDbContext.Proizvod.Where(x => request.ProizvodId == x.Id).FirstOrDefault();
            var naziv = "";
            if(proizvod!= null)
            {
                naziv = proizvod.Naziv;
            }
            return Ok(new AddKorpaArtikalResponse { Naziv=naziv});

        }
    }
}
