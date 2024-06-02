using Microsoft.Extensions.Options;
using Quartz;

namespace WebAPI.Services.Quartz
{
    public class NeaktivniKorisniciBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(NeaktivniKorisniciBackgroundJob));
            options.AddJob<NeaktivniKorisniciBackgroundJob>(jobBuilder=>jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInHours(24).RepeatForever()));
        }
    }
}
