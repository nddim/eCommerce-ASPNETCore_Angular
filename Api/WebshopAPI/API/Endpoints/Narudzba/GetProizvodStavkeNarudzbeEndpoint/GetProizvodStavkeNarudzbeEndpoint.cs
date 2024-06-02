using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.GetProizvodStavkeNarudzbeEndpoint
{
    [Route("narudzba")]
    public class GetProizvodStavkeNarudzbeEndpoint:MyBaseEndpoint<GetProizvodStavkeNarudzbeRequest, GetProizvodStavkeNarudzbeResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        public GetProizvodStavkeNarudzbeEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("get-proizvodbyId")]
        public override async Task<ActionResult<GetProizvodStavkeNarudzbeResponse>> Obradi([FromQuery]GetProizvodStavkeNarudzbeRequest request, CancellationToken cancellationToken = default)
        {
            var proizvod = await _applicationDbContext.Proizvod.FindAsync(request.Id, cancellationToken);
            if (proizvod == null)
            {
                return NotFound("Ne postoji proizvod sa tim id");
            }

            return Ok(proizvod.Naziv);
        }
    }
}
