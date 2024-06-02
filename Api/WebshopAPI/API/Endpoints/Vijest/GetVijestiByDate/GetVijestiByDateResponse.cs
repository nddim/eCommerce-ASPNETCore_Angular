namespace WebAPI.Endpoints.Vijest.GetVijestiByDate
{
    public class GetVijestiByDateResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string SlikaUrl { get; set; }
        public DateTime Datum { get; set; }
    }
}
