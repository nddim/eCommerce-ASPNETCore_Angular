namespace WebAPI.Endpoints.Vijest.GetByID
{
    public class VijestGetByIDResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public string Autor { get; set; }
        public string? SlikaUrl { get; set; }
        public int BrojKlikova { get; set; } = 0;
    }
}
