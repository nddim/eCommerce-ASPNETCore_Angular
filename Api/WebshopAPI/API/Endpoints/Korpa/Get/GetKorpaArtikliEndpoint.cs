using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korpa.Get
{
    [Authorize(Roles ="Kupac")]
    [Route("korpa")]
    public class GetKorpaArtikliEndpoint:MyBaseEndpoint<NoRequest, GetKorpaArtikliResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public GetKorpaArtikliEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpGet("getall")]
        public override async Task<ActionResult<GetKorpaArtikliResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var data = await _applicationDbContext
                .Korpa
                .Where(x => x.KupacId == user.Id)
                .Select(x => new GetKorpaArtikliResponseArray()
                {
                    Id = x.Id,
                    Proizvod = x.Proizvod,
                    Kolicina = x.Kolicina,
                    CijenaKolicina = (x.Proizvod.Popust > 0 ? x.Proizvod.Popust : x.Proizvod.PocetnaCijena) * x.Kolicina,
                    PocetnaKolicina=x.Proizvod.PocetnaKolicina
                }).ToListAsync(cancellationToken);

            var ukupno = data.Sum(x => x.CijenaKolicina);

            var podaci = new GetKorpaArtikliResponse()
            {
                Artikli = data,
                Ukupno = ukupno
            };

            return Ok(podaci);
        }
    }
}
