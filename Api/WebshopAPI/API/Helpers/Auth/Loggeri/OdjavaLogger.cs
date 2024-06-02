using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers.Auth.Loggeri.Interfacei;
using WebshopApi.Data;

namespace WebAPI.Helpers.Auth.Loggeri
{
    public class OdjavaLogger:ILoggerOdjava
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OdjavaLogger(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task logirajOdjavu (Korisnik racun)
        {
            var logOdjave = new LogOdjava()
            {
                Korisnik = racun,
                Vrijeme = DateTime.Now,
                IpAdresa = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };

            _dbContext.LogOdjava.Add(logOdjave);
            await _dbContext.SaveChangesAsync();
        }
    }
}
