using WebAPI.Helpers;

namespace WebAPI.Endpoints.Vijest.GetAll
{
    public class VijestGetAllResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public string? SlikaUrl { get; set; }
        public int BrojKlikova { get; set; } = 0;
    }

    public class VijestiGetPaged:PagedListBaseClass
    {
        public List<VijestGetAllResponse> Vijesti { get; set; }
    }
}
