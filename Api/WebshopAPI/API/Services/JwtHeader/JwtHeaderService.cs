using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Services.JwtHeader
{
    public class JwtHeaderService : IJwtHeaderService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<Korisnik> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JwtHeaderService(IHttpContextAccessor httpContext, 
            UserManager<Korisnik> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public string GetUserId()
        {
            var userId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? string.Empty;
        }

        public async Task<Korisnik> GetUser()
        {
            var userId=GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<IdentityRole> GetRoles()
        {
            var userId = GetUserId();
            var roles = await _roleManager.FindByIdAsync(userId);

            return roles;
        }

        public async Task<string> GetUserRoles()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault(); // Get the first role name in the collection
            return roleName; // Return the role name
                             //var roles = await _userManager.GetRolesAsync(user);

            //return roles.ToList(); // Convert the roles collection to a list of strings

        }
    }
}
