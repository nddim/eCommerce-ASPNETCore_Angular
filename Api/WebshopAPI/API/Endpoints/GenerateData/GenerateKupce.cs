using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.GenerateData
{
    [Microsoft.AspNetCore.Mvc.Route("generate-kupce")]
    public class GenerateKupce:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;

        public GenerateKupce(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }

        [NonAction]
        [HttpGet]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery] NoRequest noreq, CancellationToken cancellationToken = default)
        {
            var password1 = await _passwordHasher.Hash("test");
            var password2 = await _passwordHasher.Hash("test");

            _applicationDbContext.Kupac.Add(new Kupac
            {
                Ime = "Ime",
                Prezime = "Prezime",
                Adresa = "Adresa1",
                BrojTelefona = "123456",
                DatumKreiranja = DateTime.Now,
                Email = "k1@k",
                Lozinka = password1,
                isAdmin = false,
                isKupac = true,
                IsActivated = true
            });
            _applicationDbContext.Kupac.Add(new Kupac
            {
                Ime = "Ime",
                Prezime = "Prezime",
                Adresa = "Adresa2",
                BrojTelefona = "123456",
                DatumKreiranja = DateTime.Now,
                Email = "k2@k",
                Lozinka = password2,
                isAdmin = false,
                isKupac = true,
                IsActivated = true
            });
            //_applicationDbContext.Kupac.Add(new Kupac
            //{
            //    Ime = "Ime",
            //    Prezime = "Prezime",
            //    Adresa = "Adresa1",
            //    BrojTelefona = "123456",
            //    DatumKreiranja = DateTime.Now,
            //    Email = "kupac3@kupac",
            //    Lozinka = password,
            //    isAdmin = false,
            //    isKupac = true
            //});

            _applicationDbContext.SaveChanges();


            return Ok(new NoResponse());
        }
    }
}
