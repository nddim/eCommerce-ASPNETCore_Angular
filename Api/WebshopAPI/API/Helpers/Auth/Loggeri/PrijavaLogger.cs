using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers.Auth.Loggeri.Interfacei;
using WebshopApi.Data;

namespace WebAPI.Helpers.Auth.PrijavaLogger
{
    public class PrijavaLogger: ILoggerPrijava
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PrijavaLogger(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext=applicationDbContext;
            _httpContextAccessor=httpContextAccessor;
        }
        public async Task logirajPrijavu(Korisnik racun, bool uspjesno)
        {
            var logPrijave = new LogPrijava
            {
                Korisnik = racun,
                Vrijeme = DateTime.Now,
                IpAdresa = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
                Uspjesno = uspjesno
            };

            _dbContext.LogPrijava.Add(logPrijave);
            await _dbContext.SaveChangesAsync();
        }
    }
}
