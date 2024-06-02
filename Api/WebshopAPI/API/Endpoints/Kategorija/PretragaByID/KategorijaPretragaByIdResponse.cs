namespace WebAPI.Endpoints.Kategorija.PretragaByID
{
    public class KategorijaPretragaByIdResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string GlavnaKategorijaNaziv { get; set; }
        public int GlavnaKategorijaID { get; set; }
    }
}
