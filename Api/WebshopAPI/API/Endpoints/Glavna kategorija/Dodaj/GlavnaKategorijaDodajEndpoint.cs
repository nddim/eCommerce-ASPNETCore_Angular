using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models.Kategorije;
using WebAPI.Endpoints.Glavna_kategorija.Dodaj;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Glavna_kategorija.Dodaj
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("glavna-kategorija-dodaj")]
    public class GlavnaKategorijaDodajEndpoint : MyBaseEndpoint<GlavnaKategorijaDodajRequest, GlavnaKategorijaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GlavnaKategorijaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<ActionResult<GlavnaKategorijaDodajResponse>> Obradi([FromBody]GlavnaKategorijaDodajRequest request, CancellationToken cancellationToken = default)
        {
            if(request==null||request.Naziv=="")
            {
                return BadRequest("Nije unesen tekst glavne kategorije.");
            }
            var obj = new GlavnaKategorija()
            {
                Naziv = request.Naziv,
            };
            await _applicationDbContext.AddAsync(obj, cancellationToken);

            await _applicationDbContext.SaveChangesAsync();
            return Ok(new GlavnaKategorijaDodajResponse()
            {
                Id = obj.Id
            });
        }
    }
}
