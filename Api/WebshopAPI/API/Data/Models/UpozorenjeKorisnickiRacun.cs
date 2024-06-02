using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Models
{
    public class UpozorenjeKorisnickiRacun
    {
        [Key]
        public int Id { get; set; }
        public int KorisnickiRacunID { get; set; }
        [ForeignKey(nameof(KorisnickiRacunID))]
        public KorisnickiRacun KorisnickiRacun { get; set; }

        public string TipProblema { get; set; }
    }
}
