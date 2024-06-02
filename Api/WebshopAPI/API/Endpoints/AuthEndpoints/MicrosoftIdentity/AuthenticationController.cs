using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers.Auth;
using WebAPI.Helpers.Auth.EmailSlanje;
using WebAPI.Helpers.SignalR;
using WebAPI.Services.Google;
using WebAPI.Services.JwtHeader;
using WebAPI.Services.RefreshToken;
using WebshopApi.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;
        private readonly IGoogleService googleService;
        private readonly IJwtHeaderService jwtHeaderService;
        private readonly IRefreshTokenService refreshTokenService;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly IHubContext<SignalRHub> _hubContext;
        public AuthenticationController(UserManager<Korisnik> userManager,
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, 
            IEmailService emailService,
            SignInManager<Korisnik> signInManager,
            IGoogleService googleService,
            IJwtHeaderService jwtHeaderService,
            IRefreshTokenService refreshTokenService,
            ApplicationDbContext applicationDbContext,
            IHubContext<SignalRHub> hubContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.googleService = googleService;
            this.jwtHeaderService = jwtHeaderService;
            this.refreshTokenService = refreshTokenService;
            this.applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
            
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            //Provjera postoji li ovakav korisnik
            var userExist = await userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "Korisnik već postoji!", Success=false });
            }
            if (registerUser.Lozinka != registerUser.LozinkaPotvrdi)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "Lozinke se ne poklapaju!", Success =false });
            }
            //Dodavanje korisnika u bazu
            Korisnik user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Email,
                Ime = registerUser.Ime,
                Prezime = registerUser.Prezime,
                TwoFactorEnabled=true,
                DatumKreiranja=DateTime.Now
            };
            var role = "Kupac";
            if (await roleManager.RoleExistsAsync(role))
            {
                var result = await userManager.CreateAsync(user, registerUser.Lozinka);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Greška sa dodavanjem korisnika!", Success =false });
                }
                //Dodaj rolu
                await userManager.AddToRoleAsync(user, role);
                //Dodaj token za provjeru maila
                var token = TokenGenerator.Generate(256);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                await emailService.PosaljiEmail("Potvrda emaila", $"<p>Poštovani,</p><br><br>Za potvrdu maila kliknite na sljedeći link: <a href=\"{confirmationLink}\">Link</a>", user.Email);
                await Task.Run(() => emailService.PosaljiEmail("Novi korisnik", $"Ime: {registerUser.Ime}<br>Prezime: {registerUser.Prezime}<br>Email: {registerUser.Email}", "didelija.armin@gmail.com"));

                applicationDbContext.RacunAktivacija.Add(new Data.Models.RacunAktivacija { Korisnik = user, ActivateKey = token });
                await applicationDbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, new Response { Status = "2fa", Message = $"Novi račun je uspješno kreiran, a email aktivacije poslan na {user.Email}!", Success =true });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Odabrana rola ne postoji!", Success =false });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            //nadji usera
            var user = await userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Pogrešan email!", Success =false });

            //provjeri usera
            var sifraBool = await userManager.CheckPasswordAsync(user, loginUser.Lozinka);

            if (!sifraBool)
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Pogrešna lozinka!", Success =false });

            if (user.TwoFactorEnabled)
            {
                await signInManager.SignOutAsync();
                await signInManager.PasswordSignInAsync(user, loginUser.Lozinka, false, true);
                var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");
                applicationDbContext.OtpKljucevi.Add(new Otp2fa { Korisnk = user, Key = token });
                await applicationDbContext.SaveChangesAsync();
                await emailService.PosaljiEmail("2fa prijava", $"<p>Poštovani,</p><br><br>OTP ključ za vašu prijavu je: {token}", user.Email);
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "2fa", Message = $"Poslat je OTP ključ na Vaš email: {user.Email}", Success =true });
            }

            await signInManager.SignOutAsync();
            await signInManager.PasswordSignInAsync(user, loginUser.Lozinka, false, true);
            var jwtToken = await GetToken(user);
            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = GenerateRefreshToken();

            await refreshTokenService.UpdateUserRefreshToken(user, refreshToken);

            await _hubContext.Groups.AddToGroupAsync(loginUser.ConnectionId, user.Id);
            var userKonekcija = new UserKonekcija()
            {
                UserId = user.Id,
                ConnectionId = loginUser.ConnectionId,
                Vrijeme=DateTime.Now
            };
            await applicationDbContext.UserKonekcija.AddAsync(userKonekcija);
            await applicationDbContext.SaveChangesAsync();

            return Ok(new
            {
                token = encryptedToken,
                expiration = jwtToken.ValidTo,
                success=true,
                refreshToken=refreshToken.Token
            });
        }

        [HttpDelete("remove-connection")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveConnection()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var userConnection = await applicationDbContext.UserKonekcija.FirstOrDefaultAsync(c => c.UserId == userId);

            if (userConnection != null)
            {
                applicationDbContext.UserKonekcija.Remove(userConnection);
                await applicationDbContext.SaveChangesAsync();
            }

            await _hubContext.Groups.RemoveFromGroupAsync(userConnection.ConnectionId, userId);
            return Ok();
        }

        [HttpGet("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            string? refreshToken = HttpContext!.Request.Headers["refreshtoken"];
            var userBase=userManager.Users.Where(x=>x.RefreshToken==refreshToken).FirstOrDefault();

            if(userBase==null || userBase.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Refresh token je istekao, ili je korisnik odjavljen");
            }

            var jwtToken = await GetToken(userBase);

            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

     
            return Ok(new
            {
                token = encryptedToken,
                expiration = jwtToken.ValidTo,
                success = true,
            });
            return Ok();

        }
        
        [HttpDelete("revoke")]
        public async Task<IActionResult> RevokeToken(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.RefreshToken = TokenGenerator.Generate(12);
                await userManager.UpdateAsync(user);
                return Ok();

            }
            return BadRequest();

        }

        //private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        //{

        //}

        private static RefreshTokenModel GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator=RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            var refreshToken = new RefreshTokenModel
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                Token = Convert.ToBase64String(randomNumber)
            };

            return refreshToken;
        }

        [AllowAnonymous]
        [HttpPost("Login-2fa")]
        public async Task<IActionResult> Login2fa([FromBody]Login2fa login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Pogrešan email", Success =false });
            
            var signIn = await applicationDbContext.OtpKljucevi
                .Where(x => x.KorisnikId == user.Id && x.Key == login.Key && x.Valid>DateTime.Now)
                .FirstOrDefaultAsync();

            if (signIn != null)
            {           
                var jwtToken = await GetToken(user);
                var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                var refreshToken = GenerateRefreshToken();

                await refreshTokenService.UpdateUserRefreshToken(user, refreshToken);

                 return Ok(new
                {
                    token = encryptedToken,
                    expiration = jwtToken.ValidTo,
                    success=true,
                    status = "Success",
                    message = "Uspješno poslat jwt token",
                    refreshToken = refreshToken.Token

                });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Pogrešan OTP broj ili je isteklo vrijeme!", Success =false });

        }


        private async Task<JwtSecurityToken> GetToken(Korisnik user)
        {
            var authClaims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email??""),
                new (JwtRegisteredClaimNames.Name, user.UserName??""),
                new (JwtRegisteredClaimNames.NameId, user.Id??""),
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value!));

            SigningCredentials signingCred = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha512Signature);
          
            var securityToken = new JwtSecurityToken(
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(15),
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );
            return securityToken;
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await applicationDbContext.RacunAktivacija.Where(x=>x.ActivateKey==token && x.KorisnikId==user.Id).FirstOrDefaultAsync();
                if (result!= null)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                    await applicationDbContext.SaveChangesAsync();

                    //string redirectUrl = "http://localhost:4200/login";                   
                    //return Redirect(redirectUrl);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Nema korisnika sa ovim emailom", Success =false });
        }

        [NonAction]
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                await emailService.PosaljiEmail("Zaboravljena sifra", $"<p>Poštovani,</p><br><br>Za potvrdu zaboravljene sifre kliknite na link: <a href=\"{link}\">Link aktivacije</a>", user.Email);

                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = $"Mail za aktivaciju zaboravljene sifre je uspješno poslat. Uđite na ovaj mail i pogledajte poštu: {user.Email}", Success =true });

            }

            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Ne postoji korisnički račun sa unesenim mailom!", Success =false });
        }

        [NonAction]
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDTO { Token = token, Email = email };

            return Ok(new
            {
                model
            });
        }
        [NonAction]
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Lozinka);
                if(!resetPassResult.Succeeded)
                {
                    foreach(var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = $"Uspješno promijenjena šifra", Success =true });

            }

            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Ne postoji korisnički račun sa unesenim mailom!", Success =false });
        }

        [Authorize(Roles ="Kupac, Admin")]
        [HttpGet("detail")]
        public async Task<ActionResult<KorisnikGetAllDetailDto>> GetUserInfo()
        {
            //var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await userManager.FindByIdAsync(currentUserId);
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(currentUserEmail);
            if (user == null)
            {
                return NotFound(new Response { Message = "Error", Status="Pogrešan email ili ne postoji token!", Success =false });
            }

            return Ok(new KorisnikGetAllDetailDto
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email=user.Email,
                BrojTelefona = user.PhoneNumber,
                Adresa = user.Adresa,
                SaljiNovosti = user.SaljiNovosti
            });

        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getallusers")]
        public async Task<ActionResult<IEnumerable<KorisnikGetAllDetailDto>>> GetAll()
        {
            var users = await userManager.Users.Select(u => new KorisnikGetAllDetailDto
            {
                Ime = u.Ime,
                Prezime = u.Prezime,
                Email = u.Email,
                Adresa = u.Adresa,
                BrojTelefona = u.PhoneNumber,
                SaljiNovosti = u.SaljiNovosti
            }).ToListAsync();

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("google-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] GoogleLoginUser externalLoginInfo)
        {
            var payload = await googleService.VerifyGoogleToken(externalLoginInfo);

            if (payload == null)
                return BadRequest("Pogrešna externa autentifikacija!");
            var info = new UserLoginInfo(externalLoginInfo.Provider, payload.Subject, externalLoginInfo.Provider);

            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    var imePrezime = payload.Name;
                    int indeksRazmaka = payload.Name.IndexOf(' ');

                    string ime = indeksRazmaka != -1 ? imePrezime.Substring(0, indeksRazmaka).Trim() : "";
                    string prezime = indeksRazmaka != -1 ? imePrezime.Substring(indeksRazmaka).Trim() : ""; // Ostatak stringa nakon prvog razmaka

                    user = new Korisnik { Email = payload.Email, Ime = ime, Prezime=prezime, UserName = payload.Email, EmailConfirmed = true };

                    await userManager.CreateAsync(user);

                    await userManager.AddToRoleAsync(user, "Kupac");
                    await userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                return BadRequest("Pogrešna autentifikacija");

            var jwtToken = await GetToken(user);
            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = GenerateRefreshToken();

            await refreshTokenService.UpdateUserRefreshToken(user, refreshToken);

            return Ok(new { Token = encryptedToken, Success = false, RefreshToken=refreshToken.Token });
        }

        [NonAction]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return Ok();
        }
    }
}
