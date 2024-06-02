namespace WebAPI.Endpoints.Kategorija.PretragaPaged
{
    public class KategorijaPretragaPagedRequest
    {
        public string? Naziv { get; set; }
        public int? GlavnaKategorijaID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
