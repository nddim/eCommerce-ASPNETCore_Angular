using Microsoft.AspNetCore.Identity;
using Quartz;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebshopApi.Data;

namespace WebAPI.Services.Quartz
{
    [DisallowConcurrentExecution]
    public class NeaktivniKorisniciBackgroundJob : IJob
    {
        private readonly ILogger<NeaktivniKorisniciBackgroundJob> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Korisnik> _userManager;
        public NeaktivniKorisniciBackgroundJob(ILogger<NeaktivniKorisniciBackgroundJob> logger,
            ApplicationDbContext applicationDbContext, 
            UserManager<Korisnik>userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var korisnici = _applicationDbContext.Korisnik.Where(x => x.EmailConfirmed == false && x.DatumKreiranja.AddDays(30)<DateTime.Now).ToList();
            _logger.LogInformation($"Broj neaktivnih korisnika: {korisnici.Count}, Vrijeme: {DateTime.Now}");

            if (korisnici != null)
            {
                _applicationDbContext.RemoveRange(korisnici);
                _applicationDbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
