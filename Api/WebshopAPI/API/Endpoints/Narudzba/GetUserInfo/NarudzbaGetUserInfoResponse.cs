namespace WebAPI.Endpoints.Narudzba.GetUserInfo
{
    public class NarudzbaGetUserInfoResponse
    {
        public string Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string? Email { get; set; }
        public string? Adresa { get; set; }
        public string? KontaktBroj { get; set; }

    }
}
