using WebAPI.Data.Models.Kategorije;

namespace WebAPI.Endpoints.Potkategorija.PretragaHijerarhija
{
    public class PotkategorijaPretragaHijerarhijaResponse
    {
        public List<PotkategorijaPretragaHijerarhijaResponseObject> GlavneKategorije { get; set; }
    }

    public class PotkategorijaPretragaHijerarhijaResponseObject
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<KategorijaListDto> Kategorije { get; set; }
    }

    public class KategorijaListDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Potkategorija> Potkategorije { get; set; }
    }


public class Potkategorija
{
    public int Id { get; set; }
    public string Naziv { get; set; }
}
}
