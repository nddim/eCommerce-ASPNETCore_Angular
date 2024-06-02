namespace WebAPI.Endpoints.Proizvod.PretragaByNaziv
{
    public class PretragaByNazivResponse
    {
        public List<ProizvodPretragaByNazivObject> Proizvodi { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize{ get; set; }
        public int TotalCount { get; set; }
    }

    public class ProizvodPretragaByNazivObject
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PocetnaKolicina { get; set; } = 0;
        public float PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int BrojKlikova { get; set; } = 0;
        public string PotkategorijaNaziv { get; set; }
        public int PotkategorijaId { get; set; }
        public string BrendNaziv { get; set; }
        public int BrendId { get; set; }
        public string SlikaUrl { get; set; }
        public float Popust { get; set; }
        public int Relevance { get; set; }

    }
}
