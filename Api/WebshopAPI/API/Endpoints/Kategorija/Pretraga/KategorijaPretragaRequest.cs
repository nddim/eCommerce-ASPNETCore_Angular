namespace WebAPI.Endpoints.Kategorija.Pretraga
{
    public class KategorijaPretragaRequest
    {
        public string? Naziv { get; set; }
        public int? GlavnaKategorijaID { get; set; }
    }
}
