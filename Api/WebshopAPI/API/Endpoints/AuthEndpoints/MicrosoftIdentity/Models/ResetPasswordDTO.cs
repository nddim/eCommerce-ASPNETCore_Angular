using System.ComponentModel.DataAnnotations;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class ResetPasswordDTO
    {
        [Required]
        public String Lozinka { get; set; }
        [Compare("Lozinka", ErrorMessage = "Lozinka i konfirmacijska lozinka se ne poklapaju!")]
        public string LozinkaPotvrdi { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
