using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Proizvod.PretragaByNaziv;
using WebAPI.Endpoints.Proizvod.PretragaNovi;
using WebAPI.Helpers;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Proizvod.PretagaByPopust
{
    [Microsoft.AspNetCore.Mvc.Route("proizvodPopusti")]
    public class ProizvodPretragaByPopustEndpoint : MyBaseEndpoint<ProizvodPretragaByPopustRequest, ProizvodPretragaByPopustResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public ProizvodPretragaByPopustEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaByPopustResponse>> Obradi([FromQuery] ProizvodPretragaByPopustRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
                .Proizvod
                .Where(x => x.Popust > 0)
                .Select(x => new ProizvodPretragaByPopustResponseObject
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    PocetnaCijena = x.PocetnaCijena,
                    PocetnaKolicina = x.PocetnaKolicina,
                    Opis = x.Opis,
                    BrojKlikova = x.BrojKlikova,
                    PotkategorijaNaziv = x.Potkategorija.Naziv,
                    PotkategorijaID = x.PotkategorijaID,
                    BrendNaziv = x.Brend.Naziv,
                    BrendID = x.BrendID,
                    Datum = x.Datum,
                    Popust = x.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
                    DatumDodavanja=x.Datum
                });

            switch (request.SortID)
            {
                case 2:
                    query = query.OrderBy(x => x.Popust != 0 ? x.Popust : x.PocetnaCijena);
                    break;
                case 3:
                    query = query.OrderByDescending(x => x.Popust != 0 ? x.Popust : x.PocetnaCijena);
                    break;
                case 4:
                    query = query.OrderBy(x => x.Naziv);
                    break;
                case 5:
                    query = query.OrderByDescending(x => x.Naziv);
                    break;
                case 6:
                    query = query.OrderBy(x => x.DatumDodavanja);
                    break;
                case 7:
                    query = query.OrderByDescending(x => x.DatumDodavanja);
                    break;
            }

            var paged = PagedList<ProizvodPretragaByPopustResponseObject>.Create(query, request.Page, request.TableSize);

            return Ok(new ProizvodPretragaByPopustResponse()
            {
                Proizvodi = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });            
        }
    }
}
