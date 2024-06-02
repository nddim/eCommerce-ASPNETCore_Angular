namespace WebAPI.Data.Models.Kategorije
{
    public class GlavnaKategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Kategorija> Kategorije { get; set; }
    }
}
