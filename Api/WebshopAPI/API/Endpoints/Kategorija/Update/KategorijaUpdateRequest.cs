namespace WebAPI.Endpoints.Kategorija.Update
{
    public class KategorijaUpdateRequest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int GlavnaKategorijaID { get; set; }
    }
}
