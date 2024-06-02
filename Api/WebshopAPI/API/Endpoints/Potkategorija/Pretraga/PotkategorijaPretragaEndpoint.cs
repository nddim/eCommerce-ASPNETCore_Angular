using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Potkategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Potkategorija.Pretraga
{
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-pretraga")]
    public class PotkategorijaPretragaEndpoint : MyBaseEndpoint<PotkategorijaPretragaRequest, PotkategorijaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PotkategorijaPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<PotkategorijaPretragaResponse>> Obradi([FromQuery]PotkategorijaPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Potkategorija
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))&&
                (request.KategorijaID==null || request.KategorijaID==0 || request.KategorijaID==x.KategorijaID))
                .Select(x => new PotkategorijaPretragaResponse
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    KategorijaNaziv = x.Kategorija.Naziv,
                    KategorijaID=x.KategorijaID
                }).ToListAsync(cancellationToken: cancellationToken);

            return Ok(data);
        }
    }
}
