using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.Brend.Dodaj;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebshopApi.Endpoints.Brend.Dodaj
{
    [Authorize(Roles ="Admin")]
    [Microsoft.AspNetCore.Mvc.Route("brend-dodaj")]
    public class BrendDodajEndpoint : MyBaseEndpoint<BrendDodajRequest, BrendDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrendDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ActionResult<BrendDodajResponse>> Obradi([FromBody]BrendDodajRequest request, CancellationToken cancellationToken=default)
        {
            if (request == null || request.Naziv == "" || string.IsNullOrWhiteSpace(request.Naziv))
            {
                return BadRequest("Poslan je null brend ili brend bez naziva.");
            }
            var noviObj = new WebAPI.Data.Models.Brend
            {
                Naziv = request.Naziv
            };

            _applicationDbContext.Brend.Add(noviObj);

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new BrendDodajResponse
            {
                Id = noviObj.Id
            });

        }
    }
}
