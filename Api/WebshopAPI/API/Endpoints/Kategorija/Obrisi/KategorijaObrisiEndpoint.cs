using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Kategorija.Obrisi
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("kategorija-obrisi")]
    public class KategorijaObrisiEndpoint : MyBaseEndpoint<KategorijaObrisiRequest, KategorijaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KategorijaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete]
        public override async Task<ActionResult<KategorijaObrisiResponse>> Obradi([FromQuery]KategorijaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var pronadjen = await _applicationDbContext.Kategorija.FindAsync(request.Id);
            if (pronadjen == null)
            {
                return NotFound("Nema objekta u bazi");
            }

            _applicationDbContext.Kategorija.Remove(pronadjen);

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new KategorijaObrisiResponse()
            {

            });
        }
    }
}
