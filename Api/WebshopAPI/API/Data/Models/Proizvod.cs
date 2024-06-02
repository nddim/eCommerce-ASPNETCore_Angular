using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Models.Kategorije;

namespace WebAPI.Data.Models
{
    public class Proizvod
    {

        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PocetnaKolicina { get; set; } = 0;
        public float PocetnaCijena { get; set; }
        public float Popust { get; set; } = 0;
        public string Opis { get; set; }
        public DateTime Datum { get; set; }
        public int BrojKlikova { get; set; } = 0;

        public int BrendID { get; set; }
        [ForeignKey(nameof(BrendID))]
        public Brend Brend { get; set; }

        public int PotkategorijaID { get; set; }
        [ForeignKey(nameof(PotkategorijaID))]
        public Potkategorija Potkategorija { get; set; }

        public string? SlikaUrl { get; set; }
        public List<Ocjena> Ocjene { get; set; }
        public ICollection<ProizvodKlik> Klikovi { get; set; }
    }
}
