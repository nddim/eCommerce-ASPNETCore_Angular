namespace WebAPI.Endpoints.AuthEndpoints.UpdateUser
{
    public class AuthUpdateKupacRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string? BrojTelefona { get; set; }
        public string? Adresa { get; set; }
        public bool SaljiNovosti { get; set; }
    }
}
