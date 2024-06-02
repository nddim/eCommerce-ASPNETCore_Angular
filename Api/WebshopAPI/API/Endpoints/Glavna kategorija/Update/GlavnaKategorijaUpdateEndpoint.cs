using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Glavna_kategorija.Update
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("glavna-kategorija-update")]
    public class GlavnaKategorijaUpdateEndpoint : MyBaseEndpoint<GlavnaKategorijaUpdateRequest, GlavnaKategorijaUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GlavnaKategorijaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<GlavnaKategorijaUpdateResponse>> Obradi([FromBody]GlavnaKategorijaUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var glavnaKategorija = await _applicationDbContext.GlavnaKategorija.FindAsync(request.Id);

            if (glavnaKategorija == null)
            {
                return NotFound($"Nema glavne kategorija sa Id = {request.Id} u bazi.");
            }

            glavnaKategorija.Naziv = request.Naziv;

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new GlavnaKategorijaUpdateResponse()
            {
                Id = request.Id,
                Naziv=request.Naziv
            });
        }
    }
}
