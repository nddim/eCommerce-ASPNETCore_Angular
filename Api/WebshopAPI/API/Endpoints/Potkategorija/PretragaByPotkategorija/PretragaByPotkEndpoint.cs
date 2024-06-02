using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Potkategorija.PretragaByPotkategorija
{
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-pretragabypotk")]
    public class PretragaByPotkEndpoint:MyBaseEndpoint<PretragaByPotkRequest, PretragaByPotkResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PretragaByPotkEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async override Task<ActionResult<PretragaByPotkResponse>> Obradi([FromQuery]PretragaByPotkRequest request, CancellationToken cancellationToken = default)
        {
            var obj = await _applicationDbContext.Potkategorija.FindAsync(request.PotkategorijaID);
            if (obj == null)
            {
                return BadRequest("Ne postoji potkategorija sa ovim ID");
            }

            var podaci = await _applicationDbContext
                .Potkategorija
                .Where(x => x.KategorijaID == obj.KategorijaID)
                .Select(x => new PretragaByPotkResponse()
                {
                    PotkategorijaID = x.Id,
                    PotkategorijaNaziv = x.Naziv
                }).ToListAsync(cancellationToken);
            return Ok(podaci);
        }
    }
}
