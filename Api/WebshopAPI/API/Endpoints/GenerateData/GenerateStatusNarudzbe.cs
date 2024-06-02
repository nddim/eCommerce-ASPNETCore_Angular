using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.GenerateData
{
    [Route("generate-statuse")]
    public class GenerateStatusNarudzbe:MyBaseEndpoint<NoRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerateStatusNarudzbe(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [NonAction]
        [HttpGet]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var kreiran = new Data.Models.StatusNarudzbe()
            {
                Id=1,
                Status = "Kreirana"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(kreiran);
            var potvrden = new Data.Models.StatusNarudzbe()
            {
                Id = 2,

                Status = "Potvrđena"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(potvrden);
            var obrada = new Data.Models.StatusNarudzbe()
            {
                Id = 3,

                Status = "U obradi"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(obrada);
            var poslata = new Data.Models.StatusNarudzbe()
            {
                Id = 4,

                Status = "Poslata"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(poslata);
            var primljena = new Data.Models.StatusNarudzbe()
            {
                Id = 5,

                Status = "Primljena"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(primljena);
            var otkazana = new Data.Models.StatusNarudzbe()
            {
                Id = 6,

                Status = "Otkazana"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(otkazana);
            var obrisana = new Data.Models.StatusNarudzbe()
            {
                Id = 7,
                Status = "Obrisana"
            };
            await _applicationDbContext.StatusNarudzbe.AddAsync(obrisana);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new NoResponse());
        }
    }
}
