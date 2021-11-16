using Quartz;
using Quartz.Spi;
using System;


namespace Infrastructure.Quartz.JobFactory
{
  public class MessageJobFactory : IJobFactory
  {
    private readonly IServiceProvider _serviceProvider;

    public MessageJobFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
      var jobDetail = bundle.JobDetail;
      return (IJob)_serviceProvider.GetService(jobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {
      
    }
  }
}
