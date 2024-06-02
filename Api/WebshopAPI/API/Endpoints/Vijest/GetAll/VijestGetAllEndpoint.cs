using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Endpoints.Kategorija.PretragaPaged;
using WebAPI.Helpers;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Vijest.GetAll
{
    [Route("vijesti")]
    public class VijestGetAllEndpoint:MyBaseEndpoint<VijestiGetAllRequest, VijestGetAllResponse>
    {
        private ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        public VijestGetAllEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpGet("getall")]
        public override async Task<ActionResult<VijestGetAllResponse>> Obradi([FromQuery]VijestiGetAllRequest request, CancellationToken cancellationToken = default)
        {
            var query = _applicationDbContext.Vijest
                .Select(x => new VijestGetAllResponse()
                {
                    Id = x.Id,
                    Datum = x.Datum,
                    Naziv = x.Naziv,
                    Tekst = x.Tekst,
                    SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
                    BrojKlikova = x.BrojKlikova,
                })
            .OrderByDescending(x => x.Datum);

            var paged = PagedList<VijestGetAllResponse>.Create(query, request.PageNumber, request.PageSize);

            return Ok(new VijestiGetPaged()
            {
                Vijesti = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });

            //return Ok(data);
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
    }
}
