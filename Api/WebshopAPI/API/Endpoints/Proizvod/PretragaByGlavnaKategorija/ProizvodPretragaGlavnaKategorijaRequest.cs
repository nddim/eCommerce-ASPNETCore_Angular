namespace WebAPI.Endpoints.Proizvod.PretragaByGlavnaKategorija
{
    public class ProizvodPretragaGlavnaKategorijaRequest
    {
        public string? Naziv { get; set; }
        public int? GlavnaKategorijaID { get; set; }
        public int? BrendID { get; set; }
    }
}
