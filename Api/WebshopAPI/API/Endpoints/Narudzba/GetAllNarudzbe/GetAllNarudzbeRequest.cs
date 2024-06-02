namespace WebAPI.Endpoints.Narudzba.GetAllNarudzbe
{
    public class GetAllNarudzbeRequest
    {
        public int Page { get; set; } = 1;
        public int TableSize { get; set; } = 10;
        public string? Korisnik { get; set; }
    }
}
