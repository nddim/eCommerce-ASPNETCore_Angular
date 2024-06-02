using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.GenerateData
{
    [Microsoft.AspNetCore.Mvc.Route("generate-proizvode")]
    public class GenerateProizvode:MyBaseEndpoint<int, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerateProizvode(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [NonAction]
        [HttpGet]
        public override async Task<ActionResult<NoResponse>> Obradi(int BrojProizvoda, CancellationToken cancellationToken = default)
        {
            var listaPotkategorija = _applicationDbContext.Potkategorija.Select(x => x.Id).ToList();
            var listaBrendova=_applicationDbContext.Brend.Select(x=>x.Id).ToList();

            var listaProizvoda = _applicationDbContext.Proizvod.Take(20).ToList();


            for (int i = 0; i < BrojProizvoda; i++)
            {
                var rndProizvod = listaProizvoda[new Random().Next(listaProizvoda.Count)];
                var rndPotk= listaPotkategorija[new Random().Next(listaPotkategorija.Count)];
                var rndBrend= listaBrendova[new Random().Next(listaBrendova.Count)];

                var noviObj = new Data.Models.Proizvod
                {
                    Naziv = rndProizvod.Naziv,
                    BrendID = rndBrend,
                    PotkategorijaID = rndPotk,
                    BrojKlikova = 0,
                    Datum = DateTime.Now,
                    Opis = rndProizvod.Opis,
                    PocetnaCijena = new Random().Next(1500),
                    PocetnaKolicina = new Random().Next(20),
                    SlikaUrl = rndProizvod.SlikaUrl,
                    Popust = 0
                };
                await _applicationDbContext.Proizvod.AddAsync(noviObj);
            }
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new NoResponse());
        }
    }
}
