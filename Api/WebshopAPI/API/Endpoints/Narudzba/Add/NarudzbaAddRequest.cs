using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Endpoints.Narudzba.Add
{
    public class NarudzbaAddRequest
    {
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        public string Grad { get; set; }
        [Required]
        public string PostanskiBroj { get; set; }
        [Required]
        public string KontaktBroj { get; set; }
        [Required]
        public string Drzava { get; set; }
        [Required]
        public int UkupnaCijena { get; set; }
        [Required]
        public string Dostava { get; set; }
        public string KupacId { get; set; }
        public string? Komentar { get; set; }
        public List<StavkeNarudzbeDTO> StavkeNarudzbe { get; set; }

    }

    public class StavkeNarudzbeDTO
    {
        public int ProizvodId { get; set; }
        public int Kolicina { get; set; }
        public int UnitCijena { get; set; }
        public int UkupnaCijena { get; set; }
        public float Popust { get; set; }
    }
}
