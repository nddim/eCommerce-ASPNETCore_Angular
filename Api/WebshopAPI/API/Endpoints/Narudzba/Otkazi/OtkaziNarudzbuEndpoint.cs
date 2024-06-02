using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.Otkazi
{
    [Authorize(Roles ="Kupac")]
    [Route("narudzbe")]
    public class OtkaziNarudzbuEndpoint:MyBaseEndpoint<int,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public OtkaziNarudzbuEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPost("{id}")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var narudzba = await _applicationDbContext.Narudzba.Include(x => x.StatusNarudzbe).Where(x => x.Id == id && x.KupacId==user.Id).FirstOrDefaultAsync(cancellationToken);

            if (narudzba == null || narudzba.StatusNarudzbe.Status.ToLower()!="kreirana")
            {
                return BadRequest("Pogresan ID narudzbe");
            }

            var otkazana = await _applicationDbContext.StatusNarudzbe.Where(x => x.Status.ToLower() == "otkazana").FirstOrDefaultAsync(cancellationToken);

            narudzba.StatusNarudzbeId = otkazana.Id; // otkazana narudzba je ovdje sa IDom 10
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
