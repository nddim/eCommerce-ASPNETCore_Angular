using WebAPI.Helpers;

namespace WebAPI.Endpoints.Kategorija.PretragaPaged
{
    public class KategorijaPretragaPagedResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string GlavnaKategorijaNaziv { get; set; }
        public int GlavnaKategorijaID { get; set; }
    }

    public class KategorijaPretragaPagedResponseList:PagedListBaseClass
    {
        public List<KategorijaPretragaPagedResponse> Kategorije{ get; set; }
    }
}
