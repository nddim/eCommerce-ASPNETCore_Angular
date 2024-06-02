using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Data.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost { get; set; }
        [ForeignKey(nameof(korisnickiRacun))]
        public int KorisnickiRacunId { get; set; }
        public KorisnickiRacun korisnickiRacun { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string? ipAdresa { get; set; }
        [JsonIgnore]
        public string? TwoFKey { get; set; }
        public DateTime? VrijemeValidnostiTwoFKey { get; set; }
        public bool Is2FOtkljucano { get; set; }
    }
}
