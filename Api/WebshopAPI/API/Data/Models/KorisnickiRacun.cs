using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Data.Models
{
    public class KorisnickiRacun
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        
        [JsonIgnore]
        public string Lozinka { get; set; }


        public DateTime DatumKreiranja { get; set; }
        public DateTime? DatumModifikovanja { get; set; }

        public string? BrojTelefona { get; set; }
        public string? Adresa { get; set; }

        // [JsonIgnore]
        public bool isAdmin { get; set; }
        public bool isKupac { get; set; }
        public bool Is2FActive { get; set; } = false;
        public bool IsActivated { get; set; } = false;
        public bool saljiNovosti { get; set; } = false;


    }
}
