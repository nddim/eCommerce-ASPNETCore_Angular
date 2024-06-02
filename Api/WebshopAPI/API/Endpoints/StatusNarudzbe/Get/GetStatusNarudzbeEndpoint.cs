using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.StatusNarudzbe.Get
{
    [Authorize]
    [Route("status-narudzbe")]
    public class GetStatusNarudzbeEndpoint:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetStatusNarudzbeEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("get")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext.StatusNarudzbe.ToListAsync(cancellationToken);
            return Ok(data);
        }
    }
}
