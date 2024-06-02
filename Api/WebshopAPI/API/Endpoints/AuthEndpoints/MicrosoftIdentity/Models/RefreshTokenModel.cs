namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
