using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korisnici.AdminPretraga
{
    [Authorize(Roles ="Admin")]
    [Route("admini")]
    public class PretragaAdminEndpoint : MyBaseEndpoint<PretragaAdminRequest, PretragaAdminResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Korisnik> _userManager;

        public PretragaAdminEndpoint(ApplicationDbContext applicationDbContext, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public override async Task<ActionResult<PretragaAdminResponse>> Obradi([FromQuery] PretragaAdminRequest request, CancellationToken cancellationToken = default)
        {
            var usersRole = await _userManager
                .GetUsersInRoleAsync("Admin");

            var users = usersRole.ToList()
                .Select(x => new PretragaAdminResponse
                {
                    Id = x.Id,
                    BrojTelefona = x.PhoneNumber,
                    DatumKreiranja = x.DatumKreiranja,
                    Email = x.Email,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    SaljiNovosti = x.SaljiNovosti,
                });
            
            return Ok(users);
        }
    }
}
