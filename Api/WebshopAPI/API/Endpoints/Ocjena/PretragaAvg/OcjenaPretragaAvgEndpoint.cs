using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Ocjena.PretragaAvg
{
    [Route("ocjena")]
    public class OcjenaPretragaAvgEndpoint:MyBaseEndpoint<OcjenaPretragaAvgRequest, OcjenaPretragaAvgResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public OcjenaPretragaAvgEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("avg")]
        public override async Task<ActionResult<OcjenaPretragaAvgResponse>> Obradi([FromQuery] OcjenaPretragaAvgRequest request, CancellationToken cancellationToken = default)
        {
            var ocjene = await _applicationDbContext.Ocjena
                .Include(x => x.Proizvod)
                .Where(x => x.ProizvodId == request.ProizvodId)
                .ToListAsync(cancellationToken);

            double averageValue = 0f;
            
            if (ocjene != null && ocjene.Count > 0)
            {
                    averageValue = ocjene.Select(x => x.Vrijednost).Average();
            }

            var responseData = new OcjenaPretragaAvgResponse
            {
                VrijednostAvg = averageValue
            };

            return Ok(averageValue);
        }
    }
}
