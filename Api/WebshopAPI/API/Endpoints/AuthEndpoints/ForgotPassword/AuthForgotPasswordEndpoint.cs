using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.ForgotPassword
{
    [Route("auth")]
    public class AuthForgotPasswordEndpoint:MyBaseEndpoint<AuthForgotPasswordRequest, AuthForgotPasswordResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailService _emailSender;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IPasswordHasher<Korisnik> _passwordHasher;

        public AuthForgotPasswordEndpoint(ApplicationDbContext applicationDbContext,
            IEmailService emailSender,
            UserManager<Korisnik> userManager,
            IPasswordHasher<Korisnik> passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("forgot-password")]
        public override async Task<ActionResult<AuthForgotPasswordResponse>> Obradi([FromBody]AuthForgotPasswordRequest request, CancellationToken cancellationToken = default)
        {
            //var adresa = HttpContext.Connection.RemoteIpAddress?.ToString();
            //if (adresa == null)
            //    return BadRequest();

            //var korisnikLog = await _applicationDbContext
            //    .LogKretanjePoSistemu
            //    .Where(x =>
            //    x.QueryPath == "/auth/forgot-password" &&
            //    x.IpAdresa == adresa &&
            //    x.Vrijeme.AddHours(1) > DateTime.Now)
            //    .FirstOrDefaultAsync(cancellationToken);

            //if (korisnikLog != null)
            //    return BadRequest("Već ste mijenjali sifru sa uređaja u zadnjih 1h");

            var korisnik = await _applicationDbContext.Korisnik.Where(x => x.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (korisnik == null)
                return BadRequest(new AuthForgotPasswordResponse() { Uredu = false });

            var novaSifra = TokenGenerator.Generate(10);
            var hashedPassword=_passwordHasher.HashPassword(korisnik, novaSifra);
            korisnik.PasswordHash = hashedPassword;            

            //var hash = await _passwordHasher.Hash(novaSifra);
            //korisnik.Lozinka = hash;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var tekst = $"Poštovani {korisnik.Ime} {korisnik.Prezime}<br><br>" +
                        $"Vaša nova šifra je {novaSifra}<br><br>Unesite ovu šifru prilikom " +
                        $"sljedećeg prijavljivanja,<br><br>Srdačan pozdrav,<br>Webshop tim";

            await Task.Run(() => _emailSender.PosaljiEmail("Reset lozinke", tekst, korisnik.Email));

            return Ok(new AuthForgotPasswordResponse() { Uredu = true });
        }
    }
}
