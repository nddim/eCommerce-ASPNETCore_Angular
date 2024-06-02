namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class GoogleLoginUser
    {
        public string? IdToken { get; set; }
        public string? Provider { get; set; }
    }
}
