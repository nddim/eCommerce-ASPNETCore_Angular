using System.ComponentModel.DataAnnotations;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="Ime je obavezno")]
        public string? Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno")]
        public string? Prezime { get; set; }
        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezna")]
        public string? Lozinka { get; set; }
        [Required(ErrorMessage = "Potvrdna lozinka je obavezna")]
        public string? LozinkaPotvrdi { get; set; }
    }
}
