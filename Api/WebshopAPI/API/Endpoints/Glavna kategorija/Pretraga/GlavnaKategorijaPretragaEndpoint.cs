using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Glavna_kategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Glavna_kategorija.Pretraga
{
    [Microsoft.AspNetCore.Mvc.Route("glavna-kategorija-pretraga")]
    public class GlavnaKategorijaPretragaEndpoint : MyBaseEndpoint<GlavnaKategorijaPretragaRequest, GlavnaKategorijaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GlavnaKategorijaPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<GlavnaKategorijaPretragaResponse>> Obradi([FromQuery]GlavnaKategorijaPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var obj = await _applicationDbContext
                .GlavnaKategorija
                .Where(x => request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new GlavnaKategorijaPretragaResponse()
                {
                    Id = x.Id,
                    Naziv = x.Naziv
                }).ToListAsync(cancellationToken: cancellationToken);

            return Ok(obj);
        }
    }
}
