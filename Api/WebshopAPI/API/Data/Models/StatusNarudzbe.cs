using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class StatusNarudzbe
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
