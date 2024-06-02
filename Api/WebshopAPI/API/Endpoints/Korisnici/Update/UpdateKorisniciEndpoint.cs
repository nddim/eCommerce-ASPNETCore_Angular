using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korisnici.Update
{
    [Authorize(Roles = "Admin")]
    [Route("korisnici")]
    public class UpdateKorisniciEndpoint:MyBaseEndpoint<UpdateKorisniciRequest, UpdateKorisniciResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Korisnik> _userManager;

        public UpdateKorisniciEndpoint(ApplicationDbContext applicationDbContext, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpPut]
        public override async Task<ActionResult<UpdateKorisniciResponse>> Obradi([FromBody]UpdateKorisniciRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                return BadRequest("Wrong id");
            }

            user.Ime=request.Ime;
            user.Prezime=request.Prezime;
            user.PhoneNumber=request.BrojTelefona;
            user.Email = request.Email;
            user.EmailConfirmed=request.IsActivated;
            user.SaljiNovosti = request.SaljiNovosti;
            user.DatumModifikovanja=DateTime.Now;
            user.TwoFactorEnabled = request.Is2fa;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
