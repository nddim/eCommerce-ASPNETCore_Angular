using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.StatusNarudzbe.GetUserStatus
{
    [Authorize]
    [Route("status-narudzbe")]
    public class GetUserStatusNarudzbeEndpoint:MyBaseEndpoint<int, NoResponse>
    {
        private ApplicationDbContext _applicationDbContext;
        public GetUserStatusNarudzbeEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("{Id}")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromRoute]int Id, CancellationToken cancellationToken = default)
        {
            var narudzba = await _applicationDbContext.Narudzba.FindAsync(Id);

            if (narudzba == null)
            {
                return BadRequest("Nije pronadena narudzba sa tim ID");
            }

            return Ok(narudzba.StatusNarudzbeId);
        }
    }
}
