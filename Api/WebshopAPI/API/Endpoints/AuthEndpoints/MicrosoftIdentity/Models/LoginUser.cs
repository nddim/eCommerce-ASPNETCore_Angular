using System.ComponentModel.DataAnnotations;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="Email je obavezan!")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Lozinka je obavezan!")]
        public string Lozinka { get; set; }
        public string ConnectionId { get; set; }
    }
}
