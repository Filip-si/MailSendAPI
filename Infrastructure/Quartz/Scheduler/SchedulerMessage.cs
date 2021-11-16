using Infrastructure.Quartz.Model;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Quartz.Scheduler
{
  public class SchedulerMessage : IHostedService
  {
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly JobMetadata _jobMetadata;
    private readonly IJobFactory _jobFactory;

    public IScheduler Scheduler{ get; set; }


    public SchedulerMessage(ISchedulerFactory schedulerFactory, JobMetadata jobMetadata, IJobFactory jobFactory) 
    {
      _schedulerFactory = schedulerFactory;
      _jobMetadata = jobMetadata;
      _jobFactory = jobFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      // Creating schedular
      Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
      Scheduler.JobFactory = _jobFactory;

      //Creating job
      IJobDetail jobDetail = CreateJob(_jobMetadata);
      //Create trigger
      ITrigger trigger = CreateTrigger(_jobMetadata);
      //Schedule job 
      await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
      //Start the schedular
      await Scheduler.Start(cancellationToken);
    }

    private static ITrigger CreateTrigger(JobMetadata jobMetadata)
    {
      return TriggerBuilder.Create()
        .WithIdentity(jobMetadata.JobId.ToString())
        .WithCronSchedule(jobMetadata.CronExpression)
        .WithDescription(jobMetadata.JobName)
        .Build();
    }

    private static IJobDetail CreateJob(JobMetadata jobMetadata)
    {
      return JobBuilder.Create(jobMetadata.JobType)
        .WithIdentity(jobMetadata.JobId.ToString())
        .WithDescription(jobMetadata.JobName)
        .Build();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      await Scheduler.Shutdown(cancellationToken);
    }
  }
}
