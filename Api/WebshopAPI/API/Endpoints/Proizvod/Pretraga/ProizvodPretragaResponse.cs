namespace WebAPI.Endpoints.Proizvod.Pretraga
{
    public class ProizvodPretragaResponse
    {
        public List<ProizvodPretragaResponseObject> Proizvodi { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
    public class ProizvodPretragaResponseObject
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

    }
}
