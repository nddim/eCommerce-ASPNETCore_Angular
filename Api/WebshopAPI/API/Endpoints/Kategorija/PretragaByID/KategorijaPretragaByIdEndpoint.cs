using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Kategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Kategorija.PretragaByID
{
    [Microsoft.AspNetCore.Mvc.Route("kategorija-pretragabyid")]
    public class KategorijaPretragaByIdEndpoint : MyBaseEndpoint<KategorijaPretragaByIdRequest, KategorijaPretragaByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KategorijaPretragaByIdEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<KategorijaPretragaByIdResponse>> Obradi([FromQuery] KategorijaPretragaByIdRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Kategorija
                .Where(x => ((request.GlavnaKategorijaID == null ||request.GlavnaKategorijaID==0 || request.GlavnaKategorijaID==x.GlavnaKategorijaID)))
                .Select(x => new KategorijaPretragaByIdResponse
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
