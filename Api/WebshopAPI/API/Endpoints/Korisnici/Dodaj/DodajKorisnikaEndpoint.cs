using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.Auth.PasswordHasher;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Korisnici.Dodaj
{
    [Authorize(Roles = "Admin")]
    [Route("korisnici")]
    public class DodajKorisnikaEndpoint:MyBaseEndpoint<DodajKorisnikaRequest, DodajKorisnikaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailSenderService;
        private readonly UserManager<Korisnik> _userManager;

        public DodajKorisnikaEndpoint(ApplicationDbContext applicationDbContext, 
            IPasswordHasher passwordHasher, 
            IEmailService emailService,
            UserManager<Korisnik> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
            _emailSenderService = emailService;
            _userManager = userManager;
        }

        [HttpPost]
        public override async Task<ActionResult<DodajKorisnikaResponse>> Obradi(DodajKorisnikaRequest request, CancellationToken cancellationToken = default)
        {
            Korisnik user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Ime + "." + request.Prezime,
                Ime = request.Ime,
                Prezime = request.Prezime,
            };
            var role = "Admin";
            var result = await _userManager.CreateAsync(user, request.Lozinka);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Greška sa dodavanjem korisnika!", Success = false });
            }
            await _userManager.AddToRoleAsync(user, role);

            return Ok();

            //if (request.IsKupac && !request.IsAdmin)
            //{
            //    var lozinka = TokenGenerator.Generate(10);
                
            //    Korisnik user = new()
            //    {
            //        Email = request.Email,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //        UserName = request.Ime + "." + request.Prezime,
            //        Ime = request.Ime,
            //        Prezime = request.Prezime,
            //    };
            //    var role = "Admin";
            //    var result = await userManager.CreateAsync(user, registerUser.Lozinka);
            //    if (!result.Succeeded)
            //    {
            //        return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Greška sa dodavanjem korisnika!", Success = false });
            //    }

            //    //Dodaj rolu

            //    await userManager.AddToRoleAsync(user, role);

            //    //Dodaj token za provjeru maila

            //    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            //    await emailService.PosaljiEmail("Potvrda emaila", $"<p>Poštovani,</p><br><br>Za potvrdu maila kliknite na sljedeći link: <a href=\"{confirmationLink}\">Link aktivacije</a>", user.Email);

            //    //Dodaj rolu

            //    await userManager.AddToRoleAsync(user, role);
            //}


            //if (request.IsKupac && !request.IsAdmin)
            //{
            //    var lozinka = TokenGenerator.Generate(10);
            //    var hashLozinka = await _passwordHasher.Hash(lozinka);
            //    var kupac = new Kupac()
            //    {
            //        Ime = request.Ime,
            //        Prezime = request.Prezime,
            //        DatumKreiranja = DateTime.Now,
            //        DatumModifikovanja = DateTime.Now,
            //        Email = request.Email,
            //        Lozinka = hashLozinka,
            //        Is2FActive = request.Is2fa,
            //        IsActivated = true,
            //        isAdmin = false,
            //        isKupac = true,
            //        saljiNovosti = request.SaljiNovosti,
            //    };
            //    _applicationDbContext.Add(kupac);
            //    await _applicationDbContext.SaveChangesAsync(cancellationToken);
            //    var body = $"Poštovani {request.Ime} {request.Prezime},<br><br>Kreiran je Vaš novi račun sa sljedećim kredencijalima:<br>" +
            //               $"Email:{request.Email} <br>" +
            //               $"Lozinka: {lozinka}<br><br>" +
            //               $"Srdačan pozdrav,<br>" +
            //               $"Webshop tim";
            //    _emailSenderService.PosaljiEmail("Registracija računa", body, request.Email);
            //    return Ok();
            //}
            //else if (request.IsAdmin && !request.IsKupac)
            //{
            //    var lozinka = TokenGenerator.Generate(10);
            //    var hashLozinka = await _passwordHasher.Hash(lozinka);
            //    var kupac = new Administrator()
            //    {
            //        Ime = request.Ime,
            //        Prezime = request.Prezime,
            //        DatumKreiranja = DateTime.Now,
            //        DatumModifikovanja = DateTime.Now,
            //        Email = request.Email,
            //        Lozinka = hashLozinka,
            //        Is2FActive = false,
            //        IsActivated = true,
            //        isAdmin = true,
            //        isKupac = false,
            //        saljiNovosti = request.SaljiNovosti,
            //    };
            //    _applicationDbContext.Add(kupac);
            //    await _applicationDbContext.SaveChangesAsync(cancellationToken);

            //    var body = $"Poštovani {request.Ime} {request.Prezime},<br><br>Kreiran je Vaš novi račun sa sljedećim kredencijalima:<br>" +
            //               $"Email: {request.Email}<br>" +
            //               $"Lozinka: {lozinka}<br><br>" +
            //               $"Srdačan pozdrav,<br>" +
            //               $"Webshop tim";
            //    _emailSenderService.PosaljiEmail("Registracija računa", body, request.Email);
            //    return Ok();
            //}

            return BadRequest("Pogresan račun...");

        }
    }
}
