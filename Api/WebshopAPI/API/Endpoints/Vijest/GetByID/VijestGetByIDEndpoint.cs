using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.GetByID
{
    [Route("vijest")]
    public class VijestGetByIDEndpoint:MyBaseEndpoint<int, VijestGetByIDResponse>
    {
        private ApplicationDbContext _applicationDbContext;
        private IWebHostEnvironment _environment;
        public VijestGetByIDEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpGet("{id}")]
        public override async Task<ActionResult<VijestGetByIDResponse>> Obradi([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var vijest = await _applicationDbContext.Vijest.FindAsync(id);
            if (vijest == null)
            {
                return NotFound("Vijest ne postoji");
            }

            vijest.BrojKlikova++;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new VijestGetByIDResponse()
            {
                Id = vijest.Id,
                Autor = vijest.Autor,
                BrojKlikova = vijest.BrojKlikova,
                Datum = vijest.Datum,
                Naziv = vijest.Naziv,
                SlikaUrl = FajloviHelper.GetImageByProduct(vijest.SlikaUrl, _environment),
                Tekst = vijest.Tekst,
            });

        }

       
    }
}
