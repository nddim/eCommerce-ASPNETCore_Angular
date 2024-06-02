using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Kategorija.PretragaPaged;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Narudzba.GetAllNarudzbe
{
    [Authorize(Roles = "Admin")]
    [Route("narudzbe")]
    public class GetAllNarudzbeEndpoint:MyBaseEndpoint<GetAllNarudzbeRequest, GetAllNarudzbeResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetAllNarudzbeEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("getall")]
        public override async Task<ActionResult<GetAllNarudzbeResponse>> Obradi([FromQuery] GetAllNarudzbeRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext
                .Narudzba
                .Include(x => x.Kupac)
                .Where(x => (request.Korisnik == null || request.Korisnik == "") ||
                (x.Kupac.Ime + " " + x.Kupac.Prezime).ToLower().Contains(request.Korisnik.ToLower()) ||
                (x.Ime + " " + x.Prezime).ToLower().Contains(request.Korisnik.ToLower())
                )
                .Select(x => new GetAllNarudzbeResponse()
                {
                    KupacId = x.KupacId,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    DatumIsporuke = x.DatumSlanja,
                    DatumKreiranja = x.DatumKreiranja,
                    DatumPotvrde = x.DatumPotvrde,
                    Email = x.Email,
                    NarudzbaStatus = x.StatusNarudzbe.Status,
                    NarudzbaId = x.Id,
                    UkupnaCijena = x.UkupnaCijena
                });

            var ukupno = query.Count();

            query = query
                .OrderByDescending(x=>x.DatumKreiranja)
                .Skip((request.Page - 1) * request.TableSize)
                .Take(request.TableSize);

            var data=query.ToList();
            var lista=new List<NarudzbaObj>();

            foreach(var obj in data)
            {
                var stavke = await _applicationDbContext
                    .StavkeNarudzbe
                    .Include(x => x.Proizvod)
                    .Where(x => x.NarudzbaId == obj.NarudzbaId)
                    .ToListAsync(cancellationToken);

                var narudzba = new NarudzbaObj();


                narudzba.Narudzba = obj;
                narudzba.StavkeNarudzbe = new List<StavkaNarudzbeObj>();

                foreach (var stavka in stavke)
                {

                    narudzba.StavkeNarudzbe.Add(new StavkaNarudzbeObj
                    {
                        NarudzbaId=stavka.NarudzbaId,
                        Id = stavka.Id,
                        ProizvodId = stavka.ProizvodId,
                        Naziv = stavka.Proizvod.Naziv,
                        Kolicina = stavka.Kolicina,
                        CijenaProizvod = stavka.UnitCijena,
                        ZbirnoCijena = stavka.UkupnaCijena,
                    });
                }
                lista.Add(narudzba);
            }
            var stranica = 0;
            if(ukupno>0)
                stranica=ukupno/request.TableSize;


            return Ok(new NarudzbaObjResponse()
            {
                Narudzbe = lista,
                PageSize = request.TableSize,
                CurrentPage = request.Page,
                TotalCount = ukupno,
                TotalPages = stranica
            });
        }
    }

    public class NarudzbaObjResponse:PagedListBaseClass
    {
        public List<NarudzbaObj> Narudzbe { get; set; }
    }

    public class NarudzbaObj
    {
        public GetAllNarudzbeResponse Narudzba { get; set; }
        public List<StavkaNarudzbeObj> StavkeNarudzbe { get; set; }
    }

    public class StavkaNarudzbeObj
    {
        public int NarudzbaId { get; set; }
        public int Id { get; set; }
        public int ProizvodId { get; set; }
        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public float CijenaProizvod { get; set; }
        public float ZbirnoCijena { get; set; }
    }
}
