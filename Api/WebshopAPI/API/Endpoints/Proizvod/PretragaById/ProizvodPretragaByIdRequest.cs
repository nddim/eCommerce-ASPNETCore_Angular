namespace WebAPI.Endpoints.Proizvod.PretragaById
{
    public class ProizvodPretragaByIdRequest
    {
        public int Id { get; set; }
        public bool View { get; set; } = false;
    }
}
