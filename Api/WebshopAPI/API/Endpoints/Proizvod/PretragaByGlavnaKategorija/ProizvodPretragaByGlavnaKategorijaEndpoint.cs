using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaByGlavnaKategorija
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-pretragaByGlavnaKategorija")]
    public class ProizvodPretragaByGlavnaKategorijaEndpoint:MyBaseEndpoint<ProizvodPretragaGlavnaKategorijaRequest, ProizvodPretragaGlavnaKategorijaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProizvodPretragaByGlavnaKategorijaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaGlavnaKategorijaResponse>> Obradi([FromQuery]ProizvodPretragaGlavnaKategorijaRequest request, CancellationToken cancellationToken = default)
        {
            var data = await _applicationDbContext
                .Proizvod
                .Where(x => (request.Naziv == null || x.Naziv.ToLower().StartsWith(request.Naziv.ToLower())) &&
                            (request.GlavnaKategorijaID==x.Potkategorija.Kategorija.GlavnaKategorijaID) &&
                            (request.BrendID == null || request.BrendID == 0 || request.BrendID == x.BrendID))
                .Select(x => new ProizvodPretragaGlavnaKategorijaResponse()
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
