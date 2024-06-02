using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Data.Models
{
    public class Korpa
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Kupac))]
        public string KupacId { get; set; }
        public Korisnik Kupac { get; set; }

        [ForeignKey(nameof(Proizvod))]
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }

        public int Kolicina { get; set; }
        //public float CijenaKolicina { get; set; }
    }
}
