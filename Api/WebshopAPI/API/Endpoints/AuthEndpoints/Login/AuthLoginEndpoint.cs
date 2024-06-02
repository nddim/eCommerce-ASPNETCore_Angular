using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.Auth.Loggeri.Interfacei;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.Login
{
    [Microsoft.AspNetCore.Mvc.Route("auth")]
    public class AuthLoginEndpoint:MyBaseEndpoint<AuthLoginRequest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILoggerPrijava _logger;
        private readonly IPasswordHasher _passwordHasher;

        private readonly IEmailService _emailSenderService;

        //private readonly IHubContext<PorukeHub> _hubContext;


        public AuthLoginEndpoint(ApplicationDbContext applicationDbContext,
            ILoggerPrijava logPrijava,
            IPasswordHasher passwordHasher,
            IEmailService emailSenderService)//, MyEmailSenderService emailSenderService) //IHubContext<PorukeHub> hubContext
        {
            _applicationDbContext = applicationDbContext;
            _logger = logPrijava;
            _passwordHasher=passwordHasher;
            _emailSenderService = emailSenderService;
            //_hubContext = hubContext;
        }
        [NonAction]
        [HttpPost("login")]
        public override async Task<ActionResult<MyAuthInfo>> Obradi(AuthLoginRequest request, CancellationToken cancellationToken = default)
        {
            //1- provjera logina
            
            KorisnickiRacun? logiraniKorisnik = await _applicationDbContext.KorisnickiRacun
                .FirstOrDefaultAsync(k =>
                    k.Email == request.Email, cancellationToken);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return Unauthorized(new MyAuthInfo(null));
            }

            if (!logiraniKorisnik.IsActivated)
            {
                return Unauthorized(new MyAuthInfo(null));
            }
            var hashiraniPassword =await _passwordHasher.Verify(logiraniKorisnik.Lozinka, request.Lozinka);

            if (!hashiraniPassword)
            {
                return Unauthorized(new MyAuthInfo(null));
            }
            string? twoFKey = null;

            if (logiraniKorisnik.Is2FActive)
            {
                var rnd = new Random();
                twoFKey = rnd.Next(100, 999).ToString();
                var tekst = $"Poštovani {logiraniKorisnik.Ime} {logiraniKorisnik.Prezime}<br><br>" +
                        $"Vaš 2fa ključ za prijavu je: {twoFKey}<br><br>Unesite ovaj ključ na ekranu prijave" +
                        $"<br><br>Srdačan pozdrav,<br>Webshop tim";

                await _emailSenderService.PosaljiEmail("2fa kljuc", tekst, logiraniKorisnik.Email);
            }
            string randomString = TokenGenerator.Generate(32);
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiRacun = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                TwoFKey = twoFKey,
                VrijemeValidnostiTwoFKey = DateTime.Now.AddMinutes(15)
            };

            _applicationDbContext.Add(noviToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok(new MyAuthInfo(noviToken));
        }
    }
}
