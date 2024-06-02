using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Potkategorija.Update
{
    [Authorize(Roles ="Admin")]
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-update")]
    public class PotkategorijaUpdateEndpoint : MyBaseEndpoint<PotkategorijaUpdateRequest, PotkategorijaUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PotkategorijaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<PotkategorijaUpdateResponse>> Obradi([FromBody]PotkategorijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var kategorija = await _applicationDbContext.Potkategorija.FindAsync(request.Id);

            if (kategorija == null)
            {
                return NotFound($"Nema potkategorije sa Id = {request.Id} u bazi.");
            }

            kategorija.Naziv = request.Naziv;
            kategorija.KategorijaID = request.KategorijaID;

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new PotkategorijaUpdateResponse()
            {
                Id = request.Id,
                Naziv = request.Naziv
            });
        }
    }
}
