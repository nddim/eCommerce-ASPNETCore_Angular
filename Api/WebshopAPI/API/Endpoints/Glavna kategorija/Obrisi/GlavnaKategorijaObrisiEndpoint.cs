using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Glavna_kategorija.Obrisi
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("glavna-kategorija-obrisi")]
    public class GlavnaKategorijaObrisiEndpoint : MyBaseEndpoint<GlavnaKategorijaObrisiRequest, GlavnaKategorijaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GlavnaKategorijaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ActionResult<GlavnaKategorijaObrisiResponse>> Obradi([FromQuery] GlavnaKategorijaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var pronadjen = await _applicationDbContext.GlavnaKategorija.FindAsync(request.Id);
            if(pronadjen == null)
            {
                return NotFound("Nema objekta u bazi");
            }

            _applicationDbContext.GlavnaKategorija.Remove(pronadjen);

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new GlavnaKategorijaObrisiResponse()
            {

            });
        }
    }
}
