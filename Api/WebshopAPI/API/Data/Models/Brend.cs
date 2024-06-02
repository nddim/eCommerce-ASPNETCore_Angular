using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class Brend
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
