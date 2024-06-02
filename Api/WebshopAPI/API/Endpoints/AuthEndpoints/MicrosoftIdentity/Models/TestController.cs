using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebshopApi.Data;

namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Kupac")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TestController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await applicationDbContext.Brend.ToListAsync();
            return Ok(lista);
        }
    }
}
