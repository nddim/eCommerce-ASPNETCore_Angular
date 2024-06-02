using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.SignalR;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.Add
{
    [Authorize(Roles ="Kupac")]
    [Route("narudzba")]
    public class NarudzbaAddEndpoint:MyBaseEndpoint<NarudzbaAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        private readonly IHubContext<SignalRHub> _hubContext;

        public NarudzbaAddEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
            _hubContext = hubContext;
        }

        [HttpPost("dodaj")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]NarudzbaAddRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();
            var userid = _myAuthService.GetUserId();

            var rola = await _myAuthService.GetUserRoles();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var narudzba = _applicationDbContext.StatusNarudzbe.Where(x => x.Status.ToLower() == "kreirana").FirstOrDefault();

            var noviObj = new Data.Models.Narudzba()
            {
                KupacId = user.Id,
                Ime = request.Ime,
                Prezime = request.Prezime,
                Adresa = request.Adresa,
                Drzava = request.Drzava,
                Grad = request.Grad,
                PostanskiBroj = request.PostanskiBroj,
                UkupnaCijena = request.UkupnaCijena,
                StatusNarudzbeId = narudzba.Id, // staviti status narudzbe koji je odgovarajuci za kreiranu narudzbu
                Email = request.Email,
                KontaktBroj = request.KontaktBroj,
                Dostava = request.Dostava,
                DatumKreiranja = DateTime.Now,
                DatumPotvrde = null,
                DatumSlanja = null,
                Komentar = request.Komentar,                              
            };

            _applicationDbContext.Narudzba.Add(noviObj);

            var korpaArtikli = await _applicationDbContext.Korpa.Include(x => x.Proizvod)
                .Where(x => x.KupacId == user.Id)
                .ToListAsync(cancellationToken);

            foreach (var stavka in korpaArtikli)
            {
                var stavkaNarudzbe = new StavkeNarudzbe()
                {
                    ProizvodId = stavka.ProizvodId,
                    Kolicina = stavka.Kolicina,
                    UnitCijena = stavka.Proizvod.Popust > 0 ? stavka.Proizvod.Popust : stavka.Proizvod.PocetnaCijena,
                    UkupnaCijena = (stavka.Proizvod.Popust > 0 ? stavka.Proizvod.Popust : stavka.Proizvod.PocetnaCijena)*stavka.Kolicina,
                    Popust = stavka.Proizvod.Popust,
                    PocetnaCijena=stavka.Proizvod.PocetnaCijena,
                    Narudzba = noviObj
                };
                _applicationDbContext.StavkeNarudzbe.Add(stavkaNarudzbe);
            }
            await _hubContext.Clients.All.SendAsync("prijem_poruke", "narudzba poslana", cancellationToken);

            _applicationDbContext.Korpa.RemoveRange(korpaArtikli);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok(new NoResponse());
        }
    }
}
