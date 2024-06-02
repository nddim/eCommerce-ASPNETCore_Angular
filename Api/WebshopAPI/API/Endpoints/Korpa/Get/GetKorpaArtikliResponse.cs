namespace WebAPI.Endpoints.Korpa.Get
{
    public class GetKorpaArtikliResponse
    {
        public List<GetKorpaArtikliResponseArray> Artikli{ get; set; }
        public float Ukupno{ get; set; }
    }

    public class GetKorpaArtikliResponseArray
    {
        public int Id { get; set; }

        public Data.Models.Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
        public float CijenaKolicina { get; set; }
        public int PocetnaKolicina { get; set; }
    }
}
