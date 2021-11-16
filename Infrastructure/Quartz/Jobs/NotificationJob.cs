using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Quartz.Jobs
{
  public class NotificationJob : IJob
  {
    private readonly ILogger<NotificationJob> _logger;

    public NotificationJob(ILogger<NotificationJob> logger)
    {
      _logger = logger;
    }
    public Task Execute(IJobExecutionContext context)
    {
      try
      {
        
        _logger.LogInformation($"NOTIFY MESSAGE AT {DateTime.Now} and JobType: {context.JobDetail.JobType}");
      }
      catch (Exception)
      {

        throw;
      }
      return Task.CompletedTask;
    }
  }
}
