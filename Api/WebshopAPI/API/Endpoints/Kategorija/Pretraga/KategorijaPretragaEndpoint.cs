using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Kategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Kategorija.Pretraga
{
    [Microsoft.AspNetCore.Mvc.Route("kategorija-pretraga")]
    public class KategorijaPretragaEndpoint : MyBaseEndpoint<KategorijaPretragaRequest, KategorijaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KategorijaPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<KategorijaPretragaResponse>> Obradi([FromQuery]KategorijaPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Kategorija
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().Contains(request.Naziv.ToLower())) &&
                (request.GlavnaKategorijaID == null ||request.GlavnaKategorijaID==0 || request.GlavnaKategorijaID==x.GlavnaKategorijaID))
                .Select(x => new KategorijaPretragaResponse
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    GlavnaKategorijaNaziv = x.GlavnaKategorija.Naziv,
                    GlavnaKategorijaID=x.GlavnaKategorijaID
                }).ToListAsync(cancellationToken: cancellationToken);

            return Ok(data);
        }
    }
}
