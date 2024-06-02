namespace WebAPI.Endpoints.Narudzba.GetAllNarudzbe
{
    public class GetAllNarudzbeResponse
    {
        public int NarudzbaId { get; set; }
        public string KupacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime? DatumIsporuke { get; set; }
        public DateTime? DatumPotvrde { get; set; }
        public int UkupnaCijena { get; set; }
        public string NarudzbaStatus { get; set; }

    }
}
