using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Endpoints.Korisnici.AdminPretraga;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korisnici.Pretraga
{
    [Authorize(Roles="Admin")]
    [Route("korisnici")]
    public class PretragaKorisnikEndpoint:MyBaseEndpoint<PretragaKorisnikRequest, PretragaKorisnikResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Korisnik> _userManager;

        public PretragaKorisnikEndpoint(ApplicationDbContext applicationDbContext, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public override async Task<ActionResult<PretragaKorisnikResponse>> Obradi([FromQuery] PretragaKorisnikRequest request, CancellationToken cancellationToken = default)
        {
            var usersRole = await _userManager
                .GetUsersInRoleAsync("Kupac");

            var users = usersRole.ToList()
                .Select(x => new PretragaKorisnikResponse
                {
                    Id = x.Id,
                    BrojTelefona = x.PhoneNumber,
                    DatumKreiranja = x.DatumKreiranja,
                    Email = x.Email,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    SaljiNovosti = x.SaljiNovosti,
                    IsActivated=x.EmailConfirmed,
                    Is2fa=x.TwoFactorEnabled
                });

            return Ok(users);

            //var users = await _applicationDbContext
            //    .Kupac
            //    .Select(x=>new PretragaKorisnikResponse()
            //    {
            //        Id = x.Id,
            //        BrojTelefona = x.BrojTelefona,
            //        DatumKreiranja = x.DatumKreiranja,
            //        Email = x.Email,
            //        Ime = x.Ime,
            //        IsActivated = x.IsActivated,
            //        Prezime = x.Prezime,
            //        SaljiNovosti = x.saljiNovosti,
            //        Is2fa=x.Is2FActive
            //    })
            //    .ToListAsync(cancellationToken);

            //return Ok(users);
        }
    }
}
