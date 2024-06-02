namespace WebAPI.Endpoints.Korisnici.AdminPretraga
{
    public class PretragaAdminResponse
    {
        public string Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public bool IsActivated { get; set; }
        public bool SaljiNovosti { get; set; }
    }
}
