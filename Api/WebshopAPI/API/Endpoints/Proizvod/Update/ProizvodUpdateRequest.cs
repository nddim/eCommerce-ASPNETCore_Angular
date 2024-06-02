namespace WebAPI.Endpoints.Proizvod.Update
{
    public class ProizvodUpdateRequest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PocetnaKolicina { get; set; }
        public float PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int BrojKlikova { get; set; }
        public int PotkategorijaID { get; set; }
        public int BrendID { get; set; }
        public float Popust { get; set; }
        //public string SlikaUrl { get; set; }

    }
}
