using Quartz;
using WebshopApi.Data;

namespace WebAPI.Services.Quartz
{
    [DisallowConcurrentExecution]
    public class NeaktivneKonekcijeBackgroundJob:IJob
    {
        private readonly ILogger<NeaktivneKonekcijeBackgroundJob> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        public NeaktivneKonekcijeBackgroundJob(ILogger<NeaktivneKonekcijeBackgroundJob> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var konekcija = _applicationDbContext
                .UserKonekcija
                .Where(x => x.Vrijeme.AddMinutes(2) < DateTime.Now);

            _logger.LogInformation($"Broj izbrisanih konekcija: {konekcija.Count()}, Vrijeme: {DateTime.Now}");
            _applicationDbContext.RemoveRange(konekcija);
            _applicationDbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
