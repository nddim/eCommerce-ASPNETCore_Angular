namespace WebAPI.Endpoints.Proizvod.PretragaByBrends
{
    public class ProizvodPretragaByBrendRequest
    {
        public List<int>? Brendovi { get; set; }
        public int PotkategorijaID { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public int SortID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 24;
    }
}
