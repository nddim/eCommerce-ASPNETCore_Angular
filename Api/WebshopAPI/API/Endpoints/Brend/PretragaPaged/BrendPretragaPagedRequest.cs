namespace WebAPI.Endpoints.Brend.PretragaPaged
{
    public class BrendPretragaPagedRequest
    {
        public string? Naziv { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
