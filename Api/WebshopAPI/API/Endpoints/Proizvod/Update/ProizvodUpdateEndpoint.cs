using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mime;
using WebAPI.Endpoints.Potkategorija.Update;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.Update
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("proizvod-update")]
    public class ProizvodUpdateEndpoint : MyBaseEndpoint<ProizvodUpdateRequest, ProizvodUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProizvodUpdateEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public override async Task<ActionResult<ProizvodUpdateResponse>> Obradi([FromBody] ProizvodUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var proizvod = await _applicationDbContext.Proizvod.FindAsync(request.Id);

            if (proizvod == null)
            {
                return NotFound($"Nema proizvoda sa Id = {request.Id} u bazi.");
            }

            proizvod.Naziv = request.Naziv;
            proizvod.Opis = request.Opis;
            proizvod.PocetnaCijena = request.PocetnaCijena;
            proizvod.PocetnaKolicina = request.PocetnaKolicina;
            proizvod.BrojKlikova = request.BrojKlikova;
            proizvod.PotkategorijaID = request.PotkategorijaID;
            proizvod.BrendID= request.BrendID;
            proizvod.Popust=request.Popust;
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new ProizvodUpdateResponse()
            {
                Id = request.Id,
                Naziv = request.Naziv,
                Opis = request.Opis,
                PocetnaCijena = request.PocetnaCijena,
                PocetnaKolicina = request.PocetnaKolicina,
                BrojKlikova = request.BrojKlikova,
                Popust = request.Popust,
            });
        }
         public static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
        }
    }
}
