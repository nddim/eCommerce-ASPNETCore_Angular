using WebAPI.Helpers;

namespace WebAPI.Endpoints.Proizvod.PretagaByPopust
{
    public class ProizvodPretragaByPopustResponse:PagedListBaseClass
    {
        public List<ProizvodPretragaByPopustResponseObject> Proizvodi { get; set; }
    }
    public class ProizvodPretragaByPopustResponseObject
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PocetnaKolicina { get; set; } = 0;
        public float PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int BrojKlikova { get; set; } = 0;
        public DateTime Datum { get; set; }
        public string PotkategorijaNaziv { get; set; }
        public int PotkategorijaID { get; set; }
        public string BrendNaziv { get; set; }
        public int BrendID { get; set; }
        public string SlikaUrl { get; set; }
        public float Popust { get; set; }
        public DateTime DatumDodavanja { get; set; }

    }
}
