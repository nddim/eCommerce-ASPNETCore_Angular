using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.Brend.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Brend.PretragaPaged
{
    [Route("brendovi-paged")]
    public class BrendPretragaPagedEndpoint:MyBaseEndpoint<BrendPretragaPagedRequest, BrendPretragaPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrendPretragaPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ActionResult<BrendPretragaPagedResponse>> Obradi([FromQuery]BrendPretragaPagedRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
               .Brend
               .Where(x => request.Naziv == null || request.Naziv == "" || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower()))
               .Select(x => new BrendPretragaPagedResponse()
               {
                   Id = x.Id,
                   Naziv = x.Naziv
               });

            var paged = PagedList<BrendPretragaPagedResponse>.Create(query, request.PageNumber, request.PageSize);

            return Ok(new BrendPretragaPagedResponseList()
            {
                Brendovi = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });
        }
    }
}
