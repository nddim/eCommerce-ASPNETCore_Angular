using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Potkategorija.Dodaj;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Potkategorija.Dodaj
{
    [Authorize(Roles ="Admin")]
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-dodaj")]
    public class PotkategorijaDodajEndpoint : MyBaseEndpoint<PotkategorijaDodajRequest, PotkategorijaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PotkategorijaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<PotkategorijaDodajResponse>> Obradi([FromBody]PotkategorijaDodajRequest request, CancellationToken cancellationToken = default)
        {
            
            if (request == null || request.Naziv == "")
            {
                return BadRequest("Nije unesen tekst potkategorije.");
            }
            var obj = new WebAPI.Data.Models.Kategorije.Potkategorija()
            {
                Naziv = request.Naziv,
                KategorijaID = request.KategorijaID
            };

            await _applicationDbContext.AddAsync(obj, cancellationToken);
            await _applicationDbContext.SaveChangesAsync();
            var kategorija = await _applicationDbContext.Kategorija.FindAsync(request.KategorijaID);

            return Ok(new PotkategorijaDodajResponse
            {
                Id = obj.Id,
                Naziv = obj.Naziv,
                KategorijaNaziv = kategorija.Naziv
            });
        }

       
    }
}
