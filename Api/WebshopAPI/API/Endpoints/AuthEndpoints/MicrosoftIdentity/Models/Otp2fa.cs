using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class Otp2fa
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Korisnk))]
        public string KorisnikId { get; set; }
        public Korisnik Korisnk { get; set; }

        public string Key { get; set; }
        public DateTime Valid { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
