using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Potkategorija.Obrisi
{
    [Authorize(Roles ="Admin")]
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-obrisi")]
    public class PotkategorijaObrisiEndpoint : MyBaseEndpoint<PotkategorijaObrisiRequest, PotkategorijaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PotkategorijaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ActionResult<PotkategorijaObrisiResponse>> Obradi([FromQuery]PotkategorijaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var pronadjen = await _applicationDbContext.Potkategorija.FindAsync(request.Id);
            if (pronadjen == null)
            {
                return NotFound("Nema objekta u bazi");
            }

            _applicationDbContext.Potkategorija.Remove(pronadjen);

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new PotkategorijaObrisiResponse()
            {

            });
        }
    }
}
