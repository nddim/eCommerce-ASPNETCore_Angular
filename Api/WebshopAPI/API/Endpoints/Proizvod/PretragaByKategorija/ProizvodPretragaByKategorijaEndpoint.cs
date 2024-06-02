using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaByKategorija
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-pretragaByKategorija")]
    public class ProizvodPretragaByKategorijaEndpoint:MyBaseEndpoint<ProizvodPretragaByKategorijaRequest, ProizvodPretragaByKategorijaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProizvodPretragaByKategorijaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaByKategorijaResponse>> Obradi([FromQuery]ProizvodPretragaByKategorijaRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
                            (request.KategorijaID==x.Potkategorija.KategorijaID) &&
                            (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .Select(x => new ProizvodPretragaByKategorijaResponse()
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
                    Popust = x.Popust
                }).ToListAsync(cancellationToken: cancellationToken);

            return Ok(data);
        }
    }
}
