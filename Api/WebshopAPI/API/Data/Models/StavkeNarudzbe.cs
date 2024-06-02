using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class StavkeNarudzbe
    {
        [Key]
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public float UnitCijena { get; set; }
        public float UkupnaCijena { get; set; }
        public float PocetnaCijena { get; set; }
        public float Popust { get; set; }
        [ForeignKey(nameof(Proizvod))]
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
        [ForeignKey(nameof(Narudzba))]
        public int NarudzbaId { get; set; }
        public Narudzba Narudzba { get; set; }
    }
}
