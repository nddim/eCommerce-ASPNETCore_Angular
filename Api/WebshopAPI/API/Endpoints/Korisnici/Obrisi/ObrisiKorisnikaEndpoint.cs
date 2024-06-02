using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korisnici.Obrisi
{
    [Authorize(Roles ="Admin")]
    [Route("korisnici")]
    public class ObrisiKorisnikaEndpoint:MyBaseEndpoint<string, ObrisiKorisnikaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Korisnik> _userManager;

        public ObrisiKorisnikaEndpoint(ApplicationDbContext applicationDbContext, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<ObrisiKorisnikaResponse>> Obradi([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result=await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return Ok(new Response() { Status = "Success", Message = "Obrisan korisnički račun uspješno!", Success=true });
            }
            return BadRequest(new Response { Status = "Error", Message = "Greška sa brisanjem računa", Success = false });

            var korisnik = await _applicationDbContext.KorisnickiRacun.FindAsync(id);
            if (korisnik == null)
            {
                return BadRequest("Wrong id");
            }

            _applicationDbContext.Remove(korisnik);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
