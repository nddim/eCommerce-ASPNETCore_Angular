using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.Register
{
    [Route("auth")]
    public class AuthRegisterEndpoint:MyBaseEndpoint<RegisterRequest, RegisterResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly EmailSenderService _emailSender;
        private readonly IPasswordHasher _passwordHasher;


        public AuthRegisterEndpoint(ApplicationDbContext applicationDbContext, EmailSenderService emailSender, IPasswordHasher passwordHasher)//, MyEmailSenderService emailSenderService) //IHubContext<PorukeHub> hubContext
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _passwordHasher = passwordHasher;
        }

        [NonAction]
        [HttpPost("register")]
        public async override Task<ActionResult<RegisterResponse>> Obradi(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Ime == null || request.Prezime == null || request.Email == null || request.Lozinka == null || request.Lozinka!=request.PotvrdiLozinku)
                return Ok(new RegisterResponse()
                {
                    Uredu = false
                });

            var hash =await _passwordHasher.Hash(request.Lozinka);

            var kupac = new Kupac()
            {
                DatumKreiranja = DateTime.Now,
                Email = request.Email,
                Ime = request.Ime,
                Prezime = request.Prezime,
                IsActivated = false,
                Lozinka = hash,
                isAdmin = false,
                isKupac = true,
                Is2FActive = false
            };

            _applicationDbContext.Kupac.Add(kupac);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            var korisnikid = kupac.Id;
            string randomString = TokenGenerator.Generate(64);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var tekst =
                $"Poštovani {request.Ime} {request.Prezime}, <br><br>Ovo je link za registraciju vašeg računa: <a href='http://localhost:4200/activate/{randomString}'>http://localhost:4200/activate/{randomString}</a><br><br>" +
                $"Srdačan pozdrav,<br>Webshop tim";
        
            await _emailSender.PosaljiEmail("Registracija kupca", tekst, request.Email);

            return Ok(new RegisterResponse()
            {
                Uredu = true
            });

        }
    }
}
