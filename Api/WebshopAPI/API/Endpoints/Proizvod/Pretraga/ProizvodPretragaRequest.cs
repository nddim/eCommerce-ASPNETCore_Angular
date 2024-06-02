namespace WebAPI.Endpoints.Proizvod.Pretraga
{
    public class ProizvodPretragaRequest
    {
        public string? Naziv { get; set; }
        public int? PotkategorijaID { get; set; }
        public int? BrendID { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public int SortID { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 24;
    }
}
