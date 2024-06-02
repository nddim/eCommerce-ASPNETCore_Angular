namespace WebAPI.Endpoints.Korisnici.Pretraga
{
    public class PretragaKorisnikResponse
    {
        public string Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public bool IsActivated { get; set; }
        public bool SaljiNovosti { get; set; }
        public bool Is2fa { get; set; }
    }
}
