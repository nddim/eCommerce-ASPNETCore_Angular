namespace WebAPI.Endpoints.Proizvod.PretagaByPopust
{
    public class ProizvodPretragaByPopustRequest
    {
        public string? Naziv { get; set; }
        public int Page { get; set; } = 1;
        public int TableSize { get; set; } = 6;
        public int SortID { get; set; } = 1;

    }
}
