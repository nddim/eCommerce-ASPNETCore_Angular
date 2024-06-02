using System.Security.Policy;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Helpers.Report
{
    public class RacunGenerator : IRacunGenerator
    {
        private readonly IWebHostEnvironment environment;

        public RacunGenerator(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public string GenerisiRacun(Narudzba narudzba, List<StavkeNarudzbe> stavke)
        {
            var dokument = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n  <meta charset=\"utf-8\">\r\n " +
                " <title>Example 2</title>\r\n  <style>\r\n    @font-face {\r\n     " +
                " font-family: SourceSansPro;\r\n    }\r\n\r\n    .clearfix:after {\r\n      content: \"\";\r\n      display: table;\r\n " +
                "     clear: both;font-size: 20px;\r\n    }\r\n\r\n    a {\r\n      color: #0087C3;\r\n      text-decoration: none;\r\n    }\r\n\r\n    body {\r\n " +
                "     position: relative;\r\n      width: 26.5cm;\r\n      height: 38.2cm;\r\n      margin: 0 auto;\r\n      color: #555555;\r\n      " +
                "background: #FFFFFF;\r\n      font-family: Arial, sans-serif;\r\n      font-size: 16px;\r\n      font-family: SourceSansPro;\r\n    " +
                "}\r\n\r\n    header {\r\n      padding: 10px 0;\r\n      margin-bottom: 20px;\r\n      border-bottom: 1px solid #AAAAAA;\r\n   " +
                " }\r\n\r\n    #logo {\r\n      float: left;\r\n      margin-top: 8px;\r\n    }\r\n\r\n    #logo img {\r\n      height: 110px;\r\n" +
                "    }\r\n\r\n    #company {\r\n      float: right;\r\n      text-align: right;\r\n    }\r\n\r\n\r\n    #details {\r\n     " +
                " margin-bottom: 50px;\r\n    }\r\n\r\n    #client {\r\n      padding-left: 6px;\r\n      border-left: 6px solid #0087C3;\r\n " +
                "     float: left;\r\n    }\r\n\r\n    #client .to {\r\n      color: #777777; font-size:22px;\r\n    }\r\n\r\n    h2.name {\r\n     " +
                " font-size: 20px;\r\n      font-weight: normal;\r\n      margin: 0;\r\n    }\r\n\r\n    #invoice {\r\n      float: right;\r\n   " +
                "   text-align: right;\r\n    }\r\n\r\n    #invoice h1 {\r\n      color: #0087C3;\r\n      font-size: 2.4em;\r\n     " +
                " line-height: 1em;\r\n      font-weight: normal;\r\n      margin: 0 0 10px 0;\r\n    }\r\n\r\n    #invoice .date {\r\n    " +
                "  font-size: 22pxem;\r\n      color: #777777;\r\n    }\r\n\r\n    table {\r\n      width: 100%;\r\n      border-collapse: collapse;\r\n " +
                "     border-spacing: 0;\r\n      margin-bottom: 20px;\r\n    }\r\n\r\n    table th,\r\n    table td {\r\n      padding: 20px;\r\n    " +
                "  background: #EEEEEE;\r\n      text-align: center;\r\n      border-bottom: 1px solid #FFFFFF;\r\n    }\r\n\r\n    table th {\r\n  " +
                "    white-space: nowrap;\r\n      font-weight: normal;font-size:20px;\r\n    }\r\n\r\n    table td {\r\n      text-align: right;\r\n    }\r\n\r\n " +
                "   table td h3 {\r\n      color: #0D6EFD !important;\r\n      font-size: 1.2em;\r\n      font-weight: normal;\r\n    " +
                "  margin: 0 0 0.2em 0;\r\n    }\r\n\r\n    table .no {\r\n      color: #FFFFFF;\r\n      font-size: 1.6em;\r\n     " +
                " background: #0D6EFD !important;\r\n    }\r\n\r\n    table .desc {\r\n      text-align: left;\r\n    }\r\n\r\n    table .unit {\r\n  " +
                "    background: #DDDDDD;\r\n    }\r\n\r\n    table .qty {}\r\n\r\n    table .total {\r\n      background: #0D6EFD !important;\r\n  " +
                "    color: #FFFFFF;\r\n    }\r\n\r\n    table td.unit,\r\n    table td.qty,\r\n    table td.total {\r\n      font-size: 1.2em;\r\n  " +
                "  }\r\n\r\n    table tbody tr:last-child td {\r\n      border: none;\r\n    }\r\n\r\n    table tfoot td {\r\n    " +
                "  padding: 10px 20px;\r\n      background: #FFFFFF;\r\n      border-bottom: none;\r\n      font-size: 1.2em;\r\n   " +
                "   white-space: nowrap;\r\n      border-top: 1px solid #AAAAAA;\r\n    }\r\n\r\n    table tfoot tr:first-child td {\r\n  " +
                "    border-top: none;\r\n    }\r\n\r\n    table tfoot tr:last-child td {\r\n      color: #0D6EFD !important;\r\n     " +
                " font-size: 1.4em;\r\n      border-top: 1px solid #0D6EFD !important;\r\n\r\n    }\r\n\r\n    table tfoot tr td:first-child {\r\n " +
                "     border: none;\r\n    }\r\n\r\n    #thanks {\r\n      font-size: 2em;\r\n      margin-bottom: 50px;\r\n    }\r\n\r\n   " +
                " #notices {\r\n      padding-left: 6px;\r\n      border-left: 6px solid #0D6EFD !important;\r\n    }\r\n\r\n    #notices .notice {\r\n " +
                "     font-size: 1.2em;\r\n    }\r\n\r\n    footer {\r\n      color: #777777;\r\n      width: 100%;\r\n      height: 30px;font-size:20px;\r\n " +
                "     position: absolute;\r\n      bottom: 0;\r\n      border-top: 1px solid #AAAAAA;\r\n      padding: 8px 0;\r\n    " +
                "  text-align: center;\r\n    }\r\n  </style>\r\n</head>";

            dokument += "<body>";
            var slika = FajloviHelperNarudzba.GetImageByCode("Webshop.png", environment);
            dokument += $"<header class=\"clearfix\" style=\"font-size:22px\">\r\n    <div id=\"logo\">\r\n      <img src=\"{slika}\">\r\n    " +
                $"</div>\r\n    <div id=\"company\">\r\n      <h2 class=\"name\">Webshop fit</h2>\r\n " +
                $"<div>Adresa 123, Mostar</div>\r\n      <div>666 222 555</div>\r\n      <div><a>webshopapi@webshop.fit</a></div>\r\n  " +
                $"  </div>\r\n    </div>\r\n  </header>";

            dokument += $"<main>\r\n    <div id=\"details\" class=\"clearfix\">\r\n      <div id=\"client\">\r\n " +
                $"<div class=\"to\" style=\"font-size:22px\">Račun za:</div>\r\n        <h2 class=\"name\" style=\"font-size:24px\">{narudzba.Ime} {narudzba.Prezime}</h2>\r\n      " +
                $"<div class=\"address\" style=\"font-size:24px\">{narudzba.Adresa}</div>\r\n        <div class=\"email\" style=\"font-size:24px\"><a>{narudzba.Email}</a></div>\r\n  " +
                $"</div>\r\n      <div id=\"invoice\">\r\n        <h1 style=\"font-size:28px\">Račun #{narudzba.Id}</h1>\r\n        <div class=\"date\" style=\"font-size:24px\">Datum računa: {narudzba.DatumKreiranja.ToString("dd.MM.yyyy")}</div>\r\n  " +
                $"\r\n      </div>\r\n    </div>";

            dokument += $"<table  cellspacing=\"0\" cellpadding=\"0\">\r\n    " +
                $"  <thead >\r\n        <tr>\r\n          <th class=\"no\">#</th>\r\n" +
                $"          <th class=\"desc\" style=\"font-size:26px\">Opis proizvoda</th>\r\n         " +
                $" <th class=\"unit\" style=\"font-size:26px\">Cijena</th>\r\n     " +
                $"     <th class=\"qty\" style=\"font-size:26px\">Količina</th>\r\n   " +
                $"       <th class=\"total\" style=\"font-size:26px\">Ukupno</th>\r\n     " +
                $"   </tr>\r\n      </thead><tbody>";

            int c = 1;
            foreach(var s in stavke)
            {
                dokument += $"   <tr>\r\n          <td class=\"no\" style=\"font-size:26px\">{c++.ToString()}</td>\r\n     " +
                    $"     <td class=\"desc\">\r\n            <h3 style=\"font-size:26px\">{s.Proizvod.Naziv}</h3>\r\n  " +
                    $"        </td>\r\n          <td class=\"unit\" style=\"font-size:26px\">{s.UnitCijena.ToString("0.00")} KM</td>\r\n       " +
                    $"   <td class=\"qty\" style=\"font-size:26px\">{s.Kolicina}</td>\r\n          <td class=\"total\" style=\"font-size:26px\">{s.UkupnaCijena.ToString("0.00")} KM</td>\r\n        </tr>";

            }
            var porez =  narudzba.UkupnaCijena-narudzba.UkupnaCijena / 1.17;
            dokument += $" </tbody>\r\n      <tfoot>\r\n       \r\n        <tr>\r\n          <td colspan=\"2\"></td>\r\n    " +
                $"      <td colspan=\"2\" style=\"font-size:24px\">Uračunati porez (17%):</td>\r\n          <td style=\"font-size:24px\">{porez.ToString("0.00")} KM</td>\r\n   " +
                $"     </tr>\r\n        <tr>\r\n          <td colspan=\"2\"></td>\r\n        " +
                $"  <td colspan=\"2\" style=\"font-size:28px\">SVEUKUPNO</td>\r\n          <td style=\"font-size:28px\">{narudzba.UkupnaCijena.ToString("0.00")} KM</td>\r\n   " +
                $"     </tr>\r\n      </tfoot>\r\n    </table>\r\n   " +
                $" <div id=\"thanks\">Hvala na kupovini!</div>\r\n \r\n  </main>\r\n " +
                $" <footer>\r\n    Račun je kreiran elektronski i validan je bez pečata i potpisa\r\n " +
                $" </footer>\r\n</body>\r\n\r\n</html>";

            return dokument;
        }
    }

    public class FajloviHelperNarudzba
    {

        public static string GetImageByCode(string path, IWebHostEnvironment env)
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
                imageUrl = HostUrl + "/" + path;
            }

            return imageUrl;
        }

        public static string GetFilePath(string productCode, IWebHostEnvironment env, string putanja = "\\")
        {
            return env.WebRootPath + putanja + productCode;
        }
    }

}
