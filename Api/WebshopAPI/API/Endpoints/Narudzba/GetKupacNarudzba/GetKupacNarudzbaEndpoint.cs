using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Narudzba.Add;
using WebAPI.Endpoints.Narudzba.GetAllNarudzbe;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.GetKupacNarudzba
{
    [Authorize(Roles ="Admin")]
    [Route("narudzba-admin")]
    public class GetKupacNarudzbaEndpoint:MyBaseEndpoint<int, GetKupacNarudzbaResponse>
    {
        private ApplicationDbContext _applicationDbContext;
        public GetKupacNarudzbaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetKupacNarudzbaResponse>> Obradi([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var narudzba = await _applicationDbContext.Narudzba
                .Where(x=>x.Id==id)
                .Select(x => new GetKupacNarudzbaResponse()
                {
                    KupacId = x.KupacId,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Email = x.Email,
                    NarudzbaId = x.Id,
                }).FirstOrDefaultAsync(cancellationToken);
            return Ok(narudzba);
        }
    }
}
