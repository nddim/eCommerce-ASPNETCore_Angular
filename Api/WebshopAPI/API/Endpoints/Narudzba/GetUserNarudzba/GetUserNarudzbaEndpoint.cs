using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Endpoints.Kategorija.PretragaPaged;
using WebAPI.Endpoints.Narudzba.GetUserNarudzba;
using WebAPI.Helpers;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Endpoints.Narudzba.Get
{
    [Authorize(Roles ="Kupac")]
    [Route("narudzba")]
    public class GetUserNarudzbaEndpoint:MyBaseEndpoint<GetUserNarudzbaRequest, GetUserNarudzbaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _myAuthService;

        public GetUserNarudzbaEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("getusernarudzbe")]
        public override async Task<ActionResult<GetUserNarudzbaResponse>> Obradi([FromQuery] GetUserNarudzbaRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _myAuthService.GetUser();

            if (user == null)
            {
                return Ok("niste prijavljeni");
            }

            var data = _applicationDbContext.Narudzba
                .Where(x => x.KupacId == user.Id)
                .Select(x => new GetUserNarudzbaResponse()
                {
                    Id = x.Id,
                    DatumKreiranja = x.DatumKreiranja,
                    Dostava = x.Dostava,
                    StatusNarudzbe = x.StatusNarudzbe.Status,
                    UkupnaCijena = x.UkupnaCijena
                }).OrderByDescending(x=>x.DatumKreiranja);

            var paged = PagedList<GetUserNarudzbaResponse>.Create(data, request.Page, request.TableSize);

            return Ok(new GetUserNarudzbaResponseObj()
            {
                Narudzbe = paged.DataItems,
                PageSize = paged.PageSize,
                CurrentPage = paged.CurrentPage,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            });
        }
    }
}
