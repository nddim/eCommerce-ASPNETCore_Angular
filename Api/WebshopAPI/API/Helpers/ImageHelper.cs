using System.Text.RegularExpressions;

namespace WebAPI.Helpers
{
    public class ImageHelper
    {
        public static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
        }

        public static string GetImageTypeFromBase64(string base64String)
        {
            // Regularni izraz za izdvajanje tipa slike iz Base64 stringa
            var regex = new Regex(@"^data:image/(?<type>[a-zA-Z]+);base64,", RegexOptions.Compiled);

            // Pokušajte pronaći podudaranje sa regularnim izrazom
            var match = regex.Match(base64String);

            // Ako postoji podudaranje, preuzmite vrednost grupe "type"
            if (match.Success)
            {
                return match.Groups["type"].Value.ToLower();
            }

            // Ako ne uspete pronaći tip slike, vratite null ili neki podrazumevani vrednost
            return null;
        }
    }
}
