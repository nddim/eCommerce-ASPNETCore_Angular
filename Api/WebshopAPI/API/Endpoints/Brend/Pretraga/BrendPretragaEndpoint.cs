using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Brend.Pretraga;
using WebAPI.Endpoints.Proizvod.PretragaByBrends;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebshopApi.Endpoints.Brend.Pretraga
{
    [Microsoft.AspNetCore.Mvc.Route("brend-pretraga")]
    public class BrendPretragaEndpoint : MyBaseEndpoint<BrendPretragaRequest, BrendPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrendPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<BrendPretragaResponse>> Obradi([FromQuery] BrendPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var obj = _applicationDbContext
                .Brend
                .Where(x => request.Naziv == null || request.Naziv == "" || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new BrendPretragaResponse()
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToList();

            return Ok(obj);

        }
    }
}
