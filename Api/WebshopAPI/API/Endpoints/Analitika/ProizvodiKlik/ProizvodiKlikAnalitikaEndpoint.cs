using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Analitika.ProizvodiKlik
{
    [Authorize(Roles ="Admin")]
    [Route("proizvodi")]
    public class ProizvodiKlikAnalitikaEndpoint : MyBaseEndpoint<ProizvodiKlikAnalitikaRequest, ProizvodiKlikAnalitikaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProizvodiKlikAnalitikaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("analitika")]
        public override async Task<ActionResult<ProizvodiKlikAnalitikaResponse>> Obradi([FromQuery]ProizvodiKlikAnalitikaRequest request, CancellationToken cancellationToken = default)
        {
            var proizvodAnalitikaDani = ProizvodKlikAnalitikaBrojDana.Dani.FirstOrDefault(x => x.Id == request.BrojDanaId);
            if (proizvodAnalitikaDani == null)
                return BadRequest("Pogrešan broj dana!");

            var proizvod = await _applicationDbContext.Proizvod.FindAsync(request.ProizvodId);
            if (proizvod == null)
                return BadRequest("Pogrešan ID proizvoda");
       
            var datum = proizvodAnalitikaDani.DatumPocetka != DateTime.MinValue ? proizvodAnalitikaDani.DatumPocetka : _applicationDbContext.ProizvodKlik.Min(x => x.Datum);
     
            var podaci = await _applicationDbContext
                .ProizvodKlik
                .Where(x => x.ProizvodId == proizvod.Id && x.Datum > datum)
                .GroupBy(k => k.Datum.Date)
                .Select(g => new { Datum = g.Key, BrojKlikova = g.Count() })
                .ToDictionaryAsync(x => x.Datum, x => x.BrojKlikova);
       
            var lista = new ProizvodAnalitikaDTOString
            {
                ProizvodId = proizvod.Id,
                Naziv = proizvod.Naziv,
                BrojKlikova = new List<int>(),
                Datumi = new List<string>()
            };

            for (DateTime d = datum; d <= DateTime.Today; d = d.AddDays(1))
            {
                lista.Datumi.Add(d.Date.ToShortDateString());
                lista.BrojKlikova.Add(podaci.ContainsKey(d.Date) ? podaci[d.Date] : 0);
            }   
            return Ok(lista);
        }
    }

    public class ProizvodAnalitikaDTOString
    {
        public int ProizvodId { get; set; }
        public string Naziv { get; set; }
        public List<string> Datumi { get; set; }
        public List<int> BrojKlikova { get; set; }
        public List<ProizvodKlikAnalitikaBrojDana> Dani { get; set; } = ProizvodKlikAnalitikaBrojDana.Dani;

    }    
}
