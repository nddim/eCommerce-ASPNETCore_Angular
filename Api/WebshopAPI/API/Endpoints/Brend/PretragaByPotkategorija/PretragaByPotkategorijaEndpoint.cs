using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Brend.PretragaByPotkategorija
{
    [Microsoft.AspNetCore.Mvc.Route("brend-pretraga-bypotk")]
    public class PretragaByPotkategorijaEndpoint:MyBaseEndpoint<PretragaByPotkategorijaRequest, PretragaByPotkategorijaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PretragaByPotkategorijaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async override Task<ActionResult<PretragaByPotkategorijaResponse>> Obradi([FromQuery]PretragaByPotkategorijaRequest request, CancellationToken cancellationToken = default)
        {
            var brendovi = await _applicationDbContext
                .Proizvod
                .Where(x => x.PotkategorijaID == request.PotkategorijaID && ((request.min==0 && request.max==0) || (x.PocetnaCijena>=request.min && x.PocetnaCijena<=request.max)))
                .GroupBy(p => p.Brend)
                .Select(x => new PretragaByPotkategorijaResponse()
                {
                    BrendId = x.Key.Id,
                    BrendNaziv = x.Key.Naziv,
                    BrojProizvoda = x.Count()

                }).ToListAsync(cancellationToken);
            
            return Ok(brendovi);
        }
    }
}
