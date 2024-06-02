using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Data.Models
{
    public class LogOdjava
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime Vrijeme { get; set; }
        public string? IpAdresa { get; set; }
    }
}
