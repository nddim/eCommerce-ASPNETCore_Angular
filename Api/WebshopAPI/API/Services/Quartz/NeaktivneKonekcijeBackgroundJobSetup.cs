using Microsoft.Extensions.Options;
using Quartz;

namespace WebAPI.Services.Quartz
{
    public class NeaktivneKonekcijeBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(NeaktivneKonekcijeBackgroundJob));
            options.AddJob<NeaktivneKonekcijeBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInHours(2).RepeatForever()));

        }
    }
}
