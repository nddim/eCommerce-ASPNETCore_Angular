using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Proizvod.Obrisi
{
    [Authorize(Roles = "Admin")]
    [Microsoft.AspNetCore.Mvc.Route("proizvod-obrisi")]
    public class ProizvodObrisiEndpoint : MyBaseEndpoint<ProizvodObrisiRequest, ProizvodObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
            private readonly IWebHostEnvironment _webHostEnvironment;

        public ProizvodObrisiEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpDelete]
        public override async Task<ActionResult<ProizvodObrisiResponse>> Obradi([FromQuery] ProizvodObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var pronadjen = await _applicationDbContext.Proizvod.FindAsync(request.Id);
            if (pronadjen == null)
            {
                return NotFound("Nema objekta u bazi");
            }

            var url = pronadjen.SlikaUrl;

            string imageUrl = string.Empty;
            string filepath = GetFilePath(url, _webHostEnvironment);
            if (!filepath.ToLower().Contains("noimage") && System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
           
            var slike=await _applicationDbContext.ProizvodSlika.Where(x=>x.ProizvodId==pronadjen.Id).ToListAsync(cancellationToken);

            foreach (var item in slike)
            {
                string imageUrll = string.Empty;
                string filepathh = GetFilePath(item.SlikaUrl, _webHostEnvironment);
                if (!filepath.ToLower().Contains("noimage") && System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _applicationDbContext.Proizvod.Remove(pronadjen);

            await _applicationDbContext.SaveChangesAsync();

            return Ok(new ProizvodObrisiResponse()
            {

            });
        }
        public static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
        }
    }
}
