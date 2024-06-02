using WebAPI.Helpers;

namespace WebAPI.Endpoints.Potkategorija.PretragaPaged
{
    public class PotkategorijaPretragaPagedResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string KategorijaNaziv { get; set; }
        public int KategorijaID { get; set; }
    }
    public class PotkategorijaPretragaPagedResponseList:PagedListBaseClass
    {
        public List<PotkategorijaPretragaPagedResponse> Potkategorije { get; set; }
    }

}
