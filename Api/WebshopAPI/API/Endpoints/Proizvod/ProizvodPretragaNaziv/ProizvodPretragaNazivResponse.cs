namespace WebAPI.Endpoints.Proizvod.ProizvodPretragaNaziv
{
    public class ProizvodPretragaNazivResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
       
        public float PocetnaCijena { get; set; }
        public float Popust { get; set; }

        public string SlikaUrl { get; set; }
        public int Relevance { get; set; }
    }


}
