using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure
{
	public class FeedingTimeBackgroundJobSetup : IConfigureOptions<QuartzOptions>
	{
		public void Configure(QuartzOptions options)
		{
			var jobKey = JobKey.Create(nameof(FeedingTimeNotificationBackgroundJob));
			options
				.AddJob<FeedingTimeNotificationBackgroundJob>(jobBuider => jobBuider.WithIdentity(jobKey))
				.AddTrigger(trigger =>
					 trigger.ForJob(jobKey)
						.WithSimpleSchedule(schedule =>
							schedule.WithIntervalInMinutes(30).RepeatForever()));
		}
	}

}
