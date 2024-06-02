using Microsoft.AspNetCore.Identity;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Services.RefreshToken
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly UserManager<Korisnik> userManager;

        public RefreshTokenService(UserManager<Korisnik> userManager)
        {
            this.userManager = userManager;
        }
        public async Task UpdateUserRefreshToken(Korisnik user, RefreshTokenModel newRefreshToken)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.TokenExpires = newRefreshToken.Expires;
            user.TokenCreated = newRefreshToken.Created;
            await userManager.UpdateAsync(user);
        }
    }
}
