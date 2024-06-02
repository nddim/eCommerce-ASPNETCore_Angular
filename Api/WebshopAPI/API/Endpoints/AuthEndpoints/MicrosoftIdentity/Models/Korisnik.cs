using Microsoft.AspNetCore.Identity;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class Korisnik:IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string? Adresa { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumModifikovanja { get; set; }
        public bool SaljiNovosti { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
