using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.Delete
{
    [Authorize(Roles ="Admin")]
    [Route("narudzba-izbrisi")]
    public class DeleteNarudzbaEndpoint:MyBaseEndpoint<int, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DeleteNarudzbaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("{id}")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromRoute]int id, CancellationToken cancellationToken = default)
        {
            var narudzba = await _applicationDbContext.Narudzba.Include(x=>x.StatusNarudzbe).Where(x=>x.Id==id).FirstOrDefaultAsync(cancellationToken);
            if (narudzba == null)
            {
                return BadRequest("Pogresan ID narudzbe");
            } 
            if(narudzba.StatusNarudzbe.Status.ToLower() == "obrisana") // Obrisana == 11
            {
                return BadRequest("Narudzba vec obrisana");
            }

            var obrisna = await _applicationDbContext.StatusNarudzbe.Where(x => x.Status.ToLower() == "obrisana").FirstOrDefaultAsync(cancellationToken);

            narudzba.StatusNarudzbeId = obrisna.Id;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
