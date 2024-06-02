namespace WebAPI.Endpoints.Potkategorija.Update
{
    public class PotkategorijaUpdateRequest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KategorijaID { get; set; }
    }
}
