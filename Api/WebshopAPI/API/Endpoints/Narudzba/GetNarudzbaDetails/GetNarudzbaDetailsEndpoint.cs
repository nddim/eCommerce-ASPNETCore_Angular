using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.GetNarudzbaDetails
{
    [Route("narudzba")]
    public class GetNarudzbaDetailsEndpoint:MyBaseEndpoint<GetNarudzbaDetailsRequest, GetNarudzbaDetailsResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        public GetNarudzbaDetailsEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("get-details")]
        public override async Task<ActionResult<GetNarudzbaDetailsResponse>> Obradi([FromQuery]GetNarudzbaDetailsRequest request, CancellationToken cancellationToken = default)
        {
            var narudzba = await _applicationDbContext.Narudzba.FindAsync(request.Id, cancellationToken);
            if (narudzba == null)
            {
                return BadRequest($"Ne postoji narudzba sa ID {request.Id}");
            }

            var data = new GetNarudzbaDetailsResponse()
            {
                Id = narudzba.Id,
                Ime = narudzba.Ime,
                Prezime = narudzba.Prezime,
                Adresa = narudzba.Adresa,
                DatumKreiranja = narudzba.DatumKreiranja,
                DatumSlanja = narudzba. DatumSlanja,
                DatumPotvrde = narudzba.DatumPotvrde,
                Dostava = narudzba.Dostava,
                Drzava = narudzba.Drzava,
                Email = narudzba.Email,
                Grad = narudzba.Grad,
                PostanskiBroj = narudzba.PostanskiBroj,
                KontaktBroj = narudzba.KontaktBroj,
                Komentar = narudzba.Komentar,
                StatusNarudzbeId = narudzba.StatusNarudzbeId,
                UkupnaCijena = narudzba.UkupnaCijena,
            };
            if (narudzba.DatumPotvrde != null)
            {
                data.StatusPotvrden = true;
            }

            if (narudzba.DatumSlanja != null)
            {
                data.StatusSlanja = true;
            }
            return Ok(data);
        }
    }
}
