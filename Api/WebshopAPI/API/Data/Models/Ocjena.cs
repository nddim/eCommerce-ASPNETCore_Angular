using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Data.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }
        public int Vrijednost { get; set; }

        [ForeignKey(nameof(Proizvod))]
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }

        [ForeignKey(nameof(Kupac))]
        public string KupacId { get; set; }
        public Korisnik Kupac { get; set; }

    }
}
