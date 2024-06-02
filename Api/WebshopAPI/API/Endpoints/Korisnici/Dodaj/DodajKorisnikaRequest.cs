namespace WebAPI.Endpoints.Korisnici.Dodaj
{
    public class DodajKorisnikaRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public bool Is2fa { get; set; }
    }
}
