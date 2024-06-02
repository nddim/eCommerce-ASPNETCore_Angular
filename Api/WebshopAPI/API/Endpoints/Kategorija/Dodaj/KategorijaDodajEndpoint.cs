using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Kategorija.Dodaj
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("kategorija-dodaj")]
    public class KategorijaDodajEndpoint : MyBaseEndpoint<KategorijaDodajRequest, KategorijaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KategorijaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<KategorijaDodajResponse>> Obradi([FromBody]KategorijaDodajRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null || request.Naziv == "")
            {
                return BadRequest("Nije unesen tekst kategorije.");
            }
            var obj = new Data.Models.Kategorije.Kategorija()
            {
                Naziv=request.Naziv,
                GlavnaKategorijaID=request.GlavnaKategorijaID
            };

            await _applicationDbContext.AddAsync(obj, cancellationToken);

            await _applicationDbContext.SaveChangesAsync();
            var nazivGlavne = await _applicationDbContext.GlavnaKategorija.FindAsync(request.GlavnaKategorijaID);

            return Ok(new KategorijaDodajResponse
            {
                Id = obj.Id,
                Naziv= obj.Naziv,
                GlavnaKategorijaNaziv=nazivGlavne.Naziv
            });
        }
    }
}
