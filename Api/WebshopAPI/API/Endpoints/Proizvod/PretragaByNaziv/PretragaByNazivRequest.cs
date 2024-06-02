namespace WebAPI.Endpoints.Proizvod.PretragaByNaziv
{
    public class PretragaByNazivRequest
    {
        public string Naziv { get; set; }
        public int SortId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
