namespace WebAPI.Endpoints.AuthEndpoints.Register
{
    public class RegisterRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PotvrdiLozinku { get; set; }
    }
}
