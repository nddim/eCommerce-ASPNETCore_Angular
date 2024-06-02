namespace WebAPI.Endpoints.Proizvod.Dodaj
{
    public class ProizvodDodajRequest
    {
        public string Naziv { get; set; }
        public int PocetnaKolicina { get; set; } = 0;
        public float PocetnaCijena { get; set; }
        public string Opis { get; set; }
        public int BrojKlikova { get; set; } = 0;
        public int PotkategorijaID { get; set; }
        public int BrendID { get; set; }
        public float Popust { get; set; }

    }
}
