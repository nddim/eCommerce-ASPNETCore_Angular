using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebshopApi.Data;

namespace WebAPI.Endpoints.Vijest.UploadSlike
{
    [Authorize(Roles = "Admin")]
    [Route("vijest")]
    public class UploadFajla:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        public UploadFajla(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpPost("upload-slike")]
        public ActionResult UploadFile([FromForm] AddSlikaBody obj)
        {
           if (obj.Slika != null)
           {
               string projekatFolder1 = Environment.CurrentDirectory;

               string productPath = Path.Combine(projekatFolder1, @"Fajlovi\Slike\Proizvodi");

               string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Slika.FileName);
               string envFile = _environment.WebRootPath + "\\Uploads\\Images\\" + fileName;

               using (Stream fileStream = new FileStream(envFile, FileMode.Create))
               {
                   obj.Slika.CopyTo(fileStream);
               }
                var vijest = _applicationDbContext.Vijest.Find(int.Parse(obj.Id));

                if (vijest != null)
                {
                    vijest.SlikaUrl = fileName;
                }

                _applicationDbContext.SaveChanges();
                return Ok("Uspjesno postavljena slika!");

            }
            return BadRequest("Neki problem je bio");
        }



    }
}
