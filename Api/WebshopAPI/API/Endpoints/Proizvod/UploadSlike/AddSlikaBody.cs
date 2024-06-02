namespace WebAPI.Endpoints.Proizvod.UploadSlike
{
    public class AddSlikaBody
    {
        public IFormFile Slika { get; set; }
        public string Id { get; set; }
        
    }
}
