using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Data.Models
{
    public class RacunAktivacija
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public string KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public string ActivateKey { get; set; }
        public DateTime DatumKreiranja { get; set; }=DateTime.Now;
        public DateTime DatumValidnost { get; set; }=DateTime.Now.AddDays(1);
    }
}
