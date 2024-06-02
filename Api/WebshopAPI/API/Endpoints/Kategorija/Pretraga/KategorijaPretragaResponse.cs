namespace WebAPI.Endpoints.Kategorija.Pretraga
{
    public class KategorijaPretragaResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string GlavnaKategorijaNaziv { get; set; }
        public int GlavnaKategorijaID { get; set; }
    }
}
