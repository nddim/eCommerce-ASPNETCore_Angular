using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebshopApi.Data;

namespace WebAPI.Helpers.Auth
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsLogiran()
        {
            return GetAuthInfo().isLogiran;
        }

        public async Task<bool> IsAdmin()
        {
            var korisnickiRacun = GetAuthInfo().korisnickiRacun;
            if (korisnickiRacun == null)
                return false;
            var admin = await _applicationDbContext.Administrator.FindAsync(korisnickiRacun.Id);
            if (admin==null)
                return false;

            var jelAdmin = korisnickiRacun.isAdmin;
            if (!jelAdmin)
            {
                _applicationDbContext.UpozorenjeKorisnickiRacun.Add(new UpozorenjeKorisnickiRacun
                    { KorisnickiRacun = korisnickiRacun, TipProblema = "Administrator nije oznacen sa isAdmin" });
                await _applicationDbContext.SaveChangesAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> IsKupac()
        {
            var korisnickiRacun = GetAuthInfo().korisnickiRacun;
            if (korisnickiRacun == null)
                return false;
            var kupac = await _applicationDbContext.Kupac.FindAsync(korisnickiRacun.Id);
            if (kupac == null)
                return false;

            var jelKupac = korisnickiRacun.isKupac;
            if (!jelKupac)
            {
                _applicationDbContext.UpozorenjeKorisnickiRacun.Add(new UpozorenjeKorisnickiRacun
                    { KorisnickiRacun = korisnickiRacun, TipProblema = "Kupac nije oznacen sa isKupac" });
                await _applicationDbContext.SaveChangesAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> Is2fa()
        {
            var korisnickiRacun = GetAuthInfo().korisnickiRacun;
            if (korisnickiRacun == null)
                return false;
            var kupac = await _applicationDbContext.Kupac.FindAsync(korisnickiRacun.Id);
            if (kupac == null)
                return false;

            var jel2fa = korisnickiRacun.Is2FActive;
            if (!jel2fa)
            {
                return false;
            }

            return true;
        }



        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = _httpContextAccessor.HttpContext!.Request.Headers["my-auth-token"];

            AutentifikacijaToken? autentifikacijaToken = _applicationDbContext.AutentifikacijaToken
                .Include(x => x.korisnickiRacun)
                .SingleOrDefault(x => x.vrijednost == authToken);

            return new MyAuthInfo(autentifikacijaToken);
        }

        //public bool IsInRole(string role)
        //{
        //    var authInfo = GetAuthInfo();

        //    if (authInfo.korisnickiRacun == null)
        //    {
        //        return false;
        //    }

        //    // Implementirajte logiku provere rola prema vašim potrebama.
        //    // Na primer, proverite da li je korisnički račun u određenoj roli.
        //    switch (role.ToLower())
        //    {
        //        case "admin":
        //            return authInfo.korisnickiRacun.isAdmin;
        //        case "kupac":
        //            return authInfo.korisnickiRacun.isKupac;
        //        default:
        //            // Nepoznata rola
        //            return false;
        //    }
        //}
    }

    public class MyAuthInfo
    {
        public MyAuthInfo(AutentifikacijaToken? autentifikacijaToken)
        {
            this.autentifikacijaToken = autentifikacijaToken;
        }

        [JsonIgnore]
        public KorisnickiRacun? korisnickiRacun => autentifikacijaToken?.korisnickiRacun;
        public AutentifikacijaToken? autentifikacijaToken { get; set; }

        public bool isLogiran => korisnickiRacun != null;

    }
}
