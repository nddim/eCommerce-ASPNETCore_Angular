namespace WebAPI.Endpoints.Brend.PretragaByPotkategorija
{
    public class PretragaByPotkategorijaRequest
    {
        public int PotkategorijaID { get; set; }
        public float min { get; set; }
        public float max { get; set; }
    }
}
