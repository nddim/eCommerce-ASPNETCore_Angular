using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.Kategorija.PretragaPaged;
using WebAPI.Endpoints.Potkategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Potkategorija.PretragaPaged
{
    [Route("potkategorije-paged")]
    public class PotkategorijaPretragaPagedEndpoint:MyBaseEndpoint<PotkategorijaPretragaPagedRequest, PotkategorijaPretragaPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PotkategorijaPretragaPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ActionResult<PotkategorijaPretragaPagedResponse>> Obradi([FromQuery]PotkategorijaPretragaPagedRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
               .Potkategorija
               .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
               (request.KategorijaID == null || request.KategorijaID == 0 || request.KategorijaID == x.KategorijaID))
               .Select(x => new PotkategorijaPretragaPagedResponse 
               {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   KategorijaNaziv = x.Kategorija.Naziv,
                   KategorijaID = x.KategorijaID
               });

            var paged = PagedList<PotkategorijaPretragaPagedResponse>.Create(query, request.PageNumber, request.PageSize);

            return Ok(new PotkategorijaPretragaPagedResponseList()
            {
                Potkategorije = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });
        }
    }
}
