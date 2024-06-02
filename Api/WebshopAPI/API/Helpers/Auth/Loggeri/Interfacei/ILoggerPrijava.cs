using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;

namespace WebAPI.Helpers.Auth.Loggeri.Interfacei
{
    public interface ILoggerPrijava
    {
        Task logirajPrijavu(Korisnik racun, bool uspjesno);
    }
}
