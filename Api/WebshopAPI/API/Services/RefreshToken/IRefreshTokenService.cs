using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Services.RefreshToken
{
    public interface IRefreshTokenService
    {
        Task UpdateUserRefreshToken(Korisnik user, RefreshTokenModel newRefreshToken);
    }
}