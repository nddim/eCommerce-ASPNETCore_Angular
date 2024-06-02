using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.GetVijestiByDate
{
    [Route("vijesti")]
    public class GetVijestiByDateEndpoint:MyBaseEndpoint<NoRequest, GetVijestiByDateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;
        private readonly IWebHostEnvironment _environment;

        public GetVijestiByDateEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _myAuthService = myAuthService;
        }
        [HttpGet("getvijesti-bydate")]
        public override async Task<ActionResult<GetVijestiByDateResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            //if (user == null)
            //{
            //    return Ok("niste prijavljeni");
            //}

            var data = await _applicationDbContext.Vijest.Select(x => new GetVijestiByDateResponse()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Datum = x.Datum,
                SlikaUrl = FajloviHelper.GetImageByProduct(x.SlikaUrl, _environment),
            })
            .OrderByDescending(x => x.Datum)
            .Take(7)
            .ToListAsync(cancellationToken);

            return Ok(data);
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
