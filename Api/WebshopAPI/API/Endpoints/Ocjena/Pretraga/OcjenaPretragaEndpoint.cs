using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Ocjena.Pretraga
{
    [Route("ocjena")]
    public class OcjenaPretragaEndpoint:MyBaseEndpoint<OcjenaPretragaRequest, OcjenaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public OcjenaPretragaEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("getall")]
        public override async Task<ActionResult<OcjenaPretragaResponse>> Obradi([FromQuery] OcjenaPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();
            if (user == null)
            {
                return Ok("Niste prijavljeni");
            }

            var ocjena = await _applicationDbContext.Ocjena.Include(x => x.Kupac).Include(x => x.Proizvod)
                .Where(x => x.ProizvodId==request.ProizvodId).ToListAsync(cancellationToken);
            if (ocjena == null || ocjena.Count == 0)
            {
                return Ok("Nema ocjena za dati proizvod");
            }

            var data = ocjena.Select(o=> new OcjenaPretragaResponse
            {
                Id = o.Id,
                KupacId = user.Id,
                ProizvodId = o.ProizvodId,
                Vrijednost = o.Vrijednost,
            }).ToList(); 

            return Ok(data);
        }
    }
}
