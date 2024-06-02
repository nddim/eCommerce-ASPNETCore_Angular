using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SelectPdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using WebAPI.Helpers;
using WebAPI.Helpers.Report;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Narudzba.Report
{
    [Authorize]
    [Route("narudzba")]
    public class NarudzbaReportEndpoint : MyBaseEndpoint<NarudzbaReportRequest, NarudzbaReportResponse>
    {
        private readonly IJwtHeaderService _myAuthService;
        private readonly ApplicationDbContext _applicationDbContext;
        public readonly IRacunGenerator _racunGenerator;

        public NarudzbaReportEndpoint(IJwtHeaderService myAuthService, ApplicationDbContext applicationDbContext, IRacunGenerator racunGenerator)
        {
            _myAuthService = myAuthService;
            _applicationDbContext = applicationDbContext;
            _racunGenerator = racunGenerator;
        }

        [HttpGet("racun")]
        public override async Task<ActionResult<NarudzbaReportResponse>> Obradi([FromQuery]NarudzbaReportRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = await _myAuthService.GetUser();
            var narudzba = await _applicationDbContext
                .Narudzba
                .Include(x => x.Kupac)
                .Include(x => x.StatusNarudzbe)
                .Where(x => x.Id == request.NarudzbaId)
                .FirstOrDefaultAsync(cancellationToken);

            if (narudzba == null)
            {
                return BadRequest("Pogrešna narudzba ID");
            }
            if(narudzba.KupacId!=korisnik.Id)
            {
                return BadRequest("Niste kreirali ovu narudzbu!");
            }
            var stavke = await _applicationDbContext
                .StavkeNarudzbe
                .Include(x => x.Proizvod)
                .Where(x => x.NarudzbaId == request.NarudzbaId && korisnik.Id==x.Narudzba.KupacId)
                .ToListAsync(cancellationToken);

            var htmlContent = "";
            htmlContent = _racunGenerator.GenerisiRacun(narudzba, stavke);
                       
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDocument=htmlToPdf.ConvertHtmlString(htmlContent);
            
            byte[] pdf = pdfDocument.Save();
            var fileName = $"Račun - Narudzba #{narudzba.Id} - {DateTime.Now.ToString("dd.MM.yyyy")}.pdf";
            var base64=Convert.ToBase64String(pdf);
            return Ok(new NarudzbaReportResponse { File=base64, Filename=fileName });
        }
    }
}
