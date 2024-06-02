using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models
{
    public class UserKonekcija
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime Vrijeme { get; set; } = DateTime.Now;
    }
}
