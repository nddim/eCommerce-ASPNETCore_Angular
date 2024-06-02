using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.GetStavkeNarudzbe
{
    [Authorize(Roles ="Admin,Kupac")]
    [Route("narudzba-stavke")]
    public class GetStavkeNarudzbeEndpoint:MyBaseEndpoint<GetStavkeNarudzbeRequest, GetStavkeNarudzbeResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        public GetStavkeNarudzbeEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("get-stavke")]
        public override async Task<ActionResult<GetStavkeNarudzbeResponse>> Obradi([FromQuery]GetStavkeNarudzbeRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();
            var narudzba = await _applicationDbContext.Narudzba.FindAsync(request.Id, cancellationToken);
            
            if (narudzba == null)
            {
                return BadRequest($"Ne postoji narudzba sa ID {request.Id}");
            }

            if (narudzba.KupacId != user.Id)
            {
                return BadRequest("Niste kreirali ovu narudžbu");
            }

            var stavke = await _applicationDbContext.StavkeNarudzbe
                .Where(x => request.Id == x.NarudzbaId)
                .Include(x => x.Proizvod)
                .Select(x => new GetStavkeNarudzbeResponse()
                    {
                        UkupnaCijena = x.UkupnaCijena,
                        PocetnaCijena = x.PocetnaCijena,
                        Kolicina = x.Kolicina,
                        NarudzbaId = x.NarudzbaId,
                        ProizvodId = x.ProizvodId,
                        Popust = x.Popust,
                        UnitCijena = x.UnitCijena,
                        ProizvodNaziv = x.Proizvod.Naziv
                    }
                ).ToListAsync(cancellationToken);

            return Ok(stavke);
        }
    }
}
