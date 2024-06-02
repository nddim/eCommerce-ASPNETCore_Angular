using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Potkategorija.PretragaHomepage
{
    [Microsoft.AspNetCore.Mvc.Route("potkategorija-pretraga-homepage")]
    public class PotkategorijaPretragaHomepageEndpoint:MyBaseEndpoint<NoRequest, PretragaHomepageResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PotkategorijaPretragaHomepageEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<PretragaHomepageResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var potkategorije = await _applicationDbContext
                .Potkategorija
                .Select(x => new PretragaHomepageResponse()
                {
                    PotkategorijaID = x.Id,
                    PotkategorijaNaziv = x.Naziv,
                    BrojProizvoda = _applicationDbContext.Proizvod.Count(p => p.PotkategorijaID == x.Id)
                }).OrderByDescending(x => x.BrojProizvoda)
                .Take(6)
                .ToListAsync(cancellationToken);

            return Ok(potkategorije);
        }
    }
}
