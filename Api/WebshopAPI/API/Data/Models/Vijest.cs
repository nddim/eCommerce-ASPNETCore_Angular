using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class Vijest
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public string Autor { get; set; }
        public string? SlikaUrl { get; set; }
        public int BrojKlikova { get; set; } = 0;
    }
}
