using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Models.Kategorije
{
    public class Potkategorija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int KategorijaID { get; set; }
        [ForeignKey(nameof(KategorijaID))]
        public Kategorija Kategorija { get; set; }
    }
}
