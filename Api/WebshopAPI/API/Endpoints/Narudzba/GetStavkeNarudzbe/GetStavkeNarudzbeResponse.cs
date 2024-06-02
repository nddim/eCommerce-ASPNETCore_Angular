namespace WebAPI.Endpoints.Narudzba.GetStavkeNarudzbe
{
    public class GetStavkeNarudzbeResponse
    {
        public int Kolicina { get; set; }
        public float UnitCijena { get; set; }
        public float UkupnaCijena { get; set; }
        public float Popust { get; set; }
        public float PocetnaCijena { get; set; }
        public int ProizvodId { get; set; }
        public string ProizvodNaziv { get; set; }
        public int NarudzbaId { get; set; }
    }
}
