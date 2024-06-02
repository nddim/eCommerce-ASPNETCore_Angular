namespace WebAPI.Endpoints.Vijest.GetAll
{
    public class VijestiGetAllRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
