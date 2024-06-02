namespace WebAPI.Endpoints.Proizvod.PretragaByKategorija
{
    public class ProizvodPretragaByKategorijaRequest
    {
        public string? Naziv { get; set; }
        public int? KategorijaID { get; set; }
        public int? BrendID { get; set; }
    }
}
