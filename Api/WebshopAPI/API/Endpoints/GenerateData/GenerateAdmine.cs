using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.GenerateData
{
    [Microsoft.AspNetCore.Mvc.Route("generate-admine")]
    public class GenerateAdmine:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserManager<Korisnik> userManager;

        public GenerateAdmine(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher, UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
            this.userManager = userManager;
        }

        [NonAction]
        [HttpGet]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]NoRequest noreq, CancellationToken cancellationToken = default)
        {
            Korisnik user = new()
            {
                Email = "a1@webshop.fit",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin1"+ "." + "Prezime",
                Ime = "Admin1",
                Prezime = "Prezime",
                DatumKreiranja=DateTime.Now,
                DatumModifikovanja=DateTime.Now,
            };
            var role = "Admin";

            var result = await userManager.CreateAsync(user, "test");
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Greška sa dodavanjem korisnika!", Success = false });
            }

            //Dodaj rolu

            await userManager.AddToRoleAsync(user, role);

            _applicationDbContext.SaveChanges();


            return Ok(new NoResponse());

        }
    }
}
