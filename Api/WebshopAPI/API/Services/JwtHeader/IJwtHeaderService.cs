using Microsoft.AspNetCore.Identity;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Services.JwtHeader
{
    public interface IJwtHeaderService
    {
        Task<string> GetUserRoles();
        Task<IdentityRole> GetRoles();
        Task<Korisnik> GetUser();
        string GetUserId();
    }
}