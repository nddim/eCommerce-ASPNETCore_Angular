namespace WebAPI.Endpoints.Potkategorija.PretragaPaged
{
    public class PotkategorijaPretragaPagedRequest
    {
        public string? Naziv { get; set; }
        public int? KategorijaID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
