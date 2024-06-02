using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Data.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Kupac))]
        public string KupacId { get; set; }
        public Korisnik Kupac { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime? DatumPotvrde { get; set; }
        public DateTime? DatumSlanja { get; set; }

        [ForeignKey(nameof(StatusNarudzbe))]
        public int StatusNarudzbeId { get; set; }
        public StatusNarudzbe StatusNarudzbe { get; set; }
        public int UkupnaCijena { get; set; }
        public string Dostava { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string PostanskiBroj { get; set; }
        public string Drzava { get; set; }
        public string KontaktBroj { get; set; }
        public string? Komentar { get; set; }

    }
}
