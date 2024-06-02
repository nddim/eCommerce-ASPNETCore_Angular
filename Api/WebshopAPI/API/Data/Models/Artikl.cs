using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class Artikl
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
