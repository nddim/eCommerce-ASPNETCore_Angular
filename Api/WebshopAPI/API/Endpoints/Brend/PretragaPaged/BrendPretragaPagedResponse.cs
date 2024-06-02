using WebAPI.Helpers;

namespace WebAPI.Endpoints.Brend.PretragaPaged
{
    public class BrendPretragaPagedResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
    public class BrendPretragaPagedResponseList : PagedListBaseClass
    {
        public List<BrendPretragaPagedResponse> Brendovi { get; set; }
    }
}
