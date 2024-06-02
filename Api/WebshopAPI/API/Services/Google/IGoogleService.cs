using Google.Apis.Auth;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Services.Google
{
    public interface IGoogleService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginUser user);
    }
}