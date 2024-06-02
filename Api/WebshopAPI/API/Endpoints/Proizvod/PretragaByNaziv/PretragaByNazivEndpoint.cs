using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints.Proizvod.ProizvodPretragaNaziv;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.PretragaByNaziv
{
    [Route("proizvod")]
    public class PretragaByNazivEndpoint:MyBaseEndpoint<PretragaByNazivRequest, PretragaByNazivResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public PretragaByNazivEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [HttpGet("getbynaziv")]
        public override async Task<ActionResult<PretragaByNazivResponse>> Obradi([FromQuery]PretragaByNazivRequest request, CancellationToken cancellationToken = default)
        {
            int count = request.Naziv.Count(x => x == ' ');
            var query = _applicationDbContext
                .Proizvod
                .Include(x => x.Brend)
                .Include(x=>x.Potkategorija)
                //.Where(x => (x.Naziv.ToLower().Contains(request.Naziv.ToLower())))
                .Select(x => new
                {
                    Proizvod = x,
                    Relevance = RelevanceHelp(request.Naziv.ToLower(), x.Naziv.ToLower(), x.Brend.Naziv.ToLower()).Result
                })
                .AsEnumerable()
                .Select(x => new ProizvodPretragaByNazivObject()
                {
                    Id = x.Proizvod.Id,
                    Naziv = x.Proizvod.Naziv,
                    PocetnaCijena = x.Proizvod.PocetnaCijena,
                    PocetnaKolicina = x.Proizvod.PocetnaKolicina,
                    Opis = x.Proizvod.Opis,
                    BrojKlikova = x.Proizvod.BrojKlikova,
                    PotkategorijaNaziv = x.Proizvod.Potkategorija.Naziv,
                    PotkategorijaId = x.Proizvod.PotkategorijaID,
                    BrendNaziv = x.Proizvod.Brend.Naziv,
                    BrendId = x.Proizvod.BrendID,
                    Popust = x.Proizvod.Popust,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.Proizvod.SlikaUrl, _environment),
                    Relevance = x.Relevance
                })
                .Where(x => x.Relevance >= (count+1)).AsQueryable().OrderByDescending(x=>x.Relevance);


            switch (request.SortId)
            {
                case 2:
                    query = query.OrderBy(x => x.PocetnaCijena);
                    break;
                case 3:
                    query = query.OrderByDescending(x => x.PocetnaCijena);
                    break;
                case 4:
                    query = query.OrderBy(x => x.Naziv);
                    break;
                case 5:
                    query = query.OrderByDescending(x => x.PocetnaCijena);
                    break;
            }

            var paged = PagedList<ProizvodPretragaByNazivObject>.Create(query, request.PageNumber, request.PageSize);

            return Ok(new PretragaByNazivResponse()
            {
                Proizvodi = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });
        }

        private static string GetImageByProduct(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string HostUrl = "https://localhost:7110";
            string filepath = GetFilePath(path, env);
            if (!System.IO.File.Exists(filepath))
            {
                imageUrl = HostUrl + "/Uploads/Images/" + "NoImage.png";
            }
            else
            {
                imageUrl = HostUrl + "/Uploads/Images/" + path;
            }

            return imageUrl;
        }

        private static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
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
