using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.ProizvodPretragaNaziv
{
    [Route("proizvod-pretraga-search")]
    public class ProizvodPretragaNazivEndpoint:MyBaseEndpoint<ProizvodPretragaNazivRequest, ProizvodPretragaNazivResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;


        public ProizvodPretragaNazivEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet]
        public override async Task<ActionResult<ProizvodPretragaNazivResponse>> Obradi([FromQuery]ProizvodPretragaNazivRequest request, 
            CancellationToken cancellationToken = default)
        {
            if (request.Naziv.Length >= 3)
            {
                var proizvodi = _applicationDbContext
                    .Proizvod
                    .Include(x => x.Brend)
                    .Select(x => new
                    {
                        Proizvod = x,
                        Relevance = RelevanceHelp(request.Naziv.ToLower(), x.Naziv.ToLower(), x.Brend.Naziv.ToLower()).Result
                    })
                    .AsEnumerable() // Prebacujemo na izvršavanje na nivou memorije
                    .Select(x => new ProizvodPretragaNazivResponse()
                    {
                        Id = x.Proizvod.Id,
                        PocetnaCijena = x.Proizvod.PocetnaCijena,
                        Popust = x.Proizvod.Popust,
                        Naziv = x.Proizvod.Naziv,
                        SlikaUrl = FajloviHelper.GetImageByProduct(x.Proizvod.SlikaUrl, _environment),
                        Relevance = x.Relevance
                    })
                    .Where(x => x.Relevance > 0) // Filtriramo proizvode sa relevantnošću većom od 0
                    .OrderByDescending(x => x.Relevance)
                    .Take(5)
                    .ToList();
                return Ok(proizvodi);
            }

            return BadRequest("Nedovoljna dužina naziva");

        }

        public static async Task<int> RelevanceHelp(string search, string naziv, string brend)
        {
            int brojac = 0;
            var rijeci = search.Split(' ');
            foreach (var rijec in rijeci)
            {
                if (naziv.Contains(rijec))
                    brojac++;
                if (brend.Contains(rijec))
                    brojac++;
            }

            return brojac;
        }

    }
}
