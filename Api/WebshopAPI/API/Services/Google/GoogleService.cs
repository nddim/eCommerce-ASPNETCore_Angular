using Google.Apis.Auth;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Services.Google;

namespace WebAPI.Services
{
    public class GoogleService : IGoogleService
    {
        public GoogleService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginUser user)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { Configuration.GetSection("Google:ClientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(user.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
