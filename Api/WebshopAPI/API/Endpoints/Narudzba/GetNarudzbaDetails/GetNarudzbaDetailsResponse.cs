namespace WebAPI.Endpoints.Narudzba.GetNarudzbaDetails
{
    public class GetNarudzbaDetailsResponse
    {
        public int Id { get; set; }
        public int KupacId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime? DatumPotvrde { get; set; }
        public DateTime? DatumSlanja { get; set; }
        public int StatusNarudzbeId { get; set; }
        public string Dostava { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string PostanskiBroj { get; set; }
        public string Drzava { get; set; }
        public string KontaktBroj { get; set; }
        public string? Komentar { get; set; }
        public bool StatusPotvrden { get; set; }
        public bool StatusSlanja { get; set; }
        public float UkupnaCijena { get; set; }
    }

}
