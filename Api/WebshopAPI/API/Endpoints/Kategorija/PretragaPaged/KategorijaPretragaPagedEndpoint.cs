using Microsoft.AspNetCore.Mvc;
using WebAPI.Endpoints.Brend.PretragaPaged;
using WebAPI.Endpoints.Kategorija.Pretraga;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Kategorija.PretragaPaged
{
    [Route("kategorije-paged")]
    public class KategorijaPretragaPagedEndpoint:MyBaseEndpoint<KategorijaPretragaPagedRequest, KategorijaPretragaPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KategorijaPretragaPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ActionResult<KategorijaPretragaPagedResponse>> Obradi([FromQuery]KategorijaPretragaPagedRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
               .Kategorija
               .Where(x => (request.Naziv == null || x.Naziv.ToLower().Contains(request.Naziv.ToLower())) &&
               (request.GlavnaKategorijaID == null || request.GlavnaKategorijaID == 0 || request.GlavnaKategorijaID == x.GlavnaKategorijaID))
               .Select(x => new KategorijaPretragaPagedResponse
               {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   GlavnaKategorijaNaziv = x.GlavnaKategorija.Naziv,
                   GlavnaKategorijaID = x.GlavnaKategorijaID
               });

            var paged = PagedList<KategorijaPretragaPagedResponse>.Create(query, request.PageNumber, request.PageSize);

            return Ok(new KategorijaPretragaPagedResponseList()
            {
                Kategorije = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });
        }
    }
}
