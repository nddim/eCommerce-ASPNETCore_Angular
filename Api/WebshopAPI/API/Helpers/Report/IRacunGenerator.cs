using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Helpers.Report
{
    public interface IRacunGenerator
    {
        public string GenerisiRacun(Narudzba narudzba, List<StavkeNarudzbe> stavke);
        
    }
}