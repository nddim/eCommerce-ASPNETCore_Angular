using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.Dodaj
{
    [Microsoft.AspNetCore.Mvc.Route("proizvod-dodaj")]
    public class ProizvodDodajEndpoint : MyBaseEndpoint<ProizvodDodajRequest, ProizvodDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProizvodDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [NonAction]
        [HttpPost]
        public override async Task<ActionResult<ProizvodDodajResponse>> Obradi([FromBody] ProizvodDodajRequest request, CancellationToken cancellationToken = default)
        {
            
            if (request == null || request.Naziv == "")
            {
                return BadRequest("Nije unesen tekst potkategorije.");
            }
            var obj = new WebAPI.Data.Models.Proizvod()
            {
                Naziv = request.Naziv,
                PotkategorijaID = request.PotkategorijaID,
                BrendID = request.BrendID,
                PocetnaKolicina = request.PocetnaKolicina,
                PocetnaCijena = request.PocetnaCijena,
                Opis = request.Opis,
                BrojKlikova = request.BrojKlikova,
                Datum = DateTime.Now,
                Popust = request.Popust,
            };

            await _applicationDbContext.AddAsync(obj, cancellationToken);
            await _applicationDbContext.SaveChangesAsync();
            //var kategorija = await _applicationDbContext.Kategorija.FindAsync(request.KategorijaID);
            var potkategorija = await _applicationDbContext.Potkategorija.FindAsync(request.PotkategorijaID);
            var brend = await _applicationDbContext.Brend.FindAsync(request.BrendID);

            //var proizvod = await _applicationDbContext.Proizvod.FirstOrDefaultAsync(p => p.PotkategorijaID == request.PotkategorijaID && p.BrendID == request.BrendID);
            return Ok(new ProizvodDodajResponse
            {
                Id = obj.Id,
                Naziv = obj.Naziv,
                PocetnaKolicina = obj.PocetnaKolicina,
                PocetnaCijena = obj.PocetnaCijena,
                Opis = obj.Opis,
                BrojKlikova = obj.BrojKlikova,
                PotkategorijaNaziv = potkategorija.Naziv,
                BrendNaziv = brend.Naziv,
                Popust = obj.Popust
                
                //KategorijaNaziv = kategorija.Naziv
            });
        }

       
    }
}
