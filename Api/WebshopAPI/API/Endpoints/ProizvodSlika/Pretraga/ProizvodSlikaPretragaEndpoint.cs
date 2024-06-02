using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.ProizvodSlika.Pretraga
{
    [Route("proizvodslika")]
    public class ProizvodSlikaPretragaEndpoint : MyBaseEndpoint<ProizvodSLikaPretragaRequest, ProizvodSlikaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProizvodSlikaPretragaEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet()]
        public override async Task<ActionResult<ProizvodSlikaPretragaResponse>> Obradi([FromQuery]ProizvodSLikaPretragaRequest request, CancellationToken cancellationToken = default)
        {
            var proizvodSlike = await _applicationDbContext
                .ProizvodSlika
                .Where(x => x.ProizvodId == request.ProizvodId)
                .ToListAsync(cancellationToken);

            var lista=new List<ProizvodSlikaPretragaResponse>();

            foreach(var ps in proizvodSlike)
            {
                var slikaUrl=FajloviHelper.GetImageByProduct(ps.SlikaUrl, _webHostEnvironment);
                lista.Add(new ProizvodSlikaPretragaResponse { Id=ps.Id, SlikaUrl = slikaUrl });
            }

            return Ok(lista);
        }
    }
}
