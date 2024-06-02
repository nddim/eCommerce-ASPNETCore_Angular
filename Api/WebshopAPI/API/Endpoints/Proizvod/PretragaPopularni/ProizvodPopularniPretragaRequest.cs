namespace WebAPI.Endpoints.Proizvod.PretragaPopularni
{
    public class ProizvodPopularniPretragaRequest
    {
        public string? Naziv { get; set; }
        public int? PotkategorijaID { get; set; }
        public int? BrendID { get; set; }

    }
}
