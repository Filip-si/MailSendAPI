using Infrastructure.Quartz.JobFactory;
using Infrastructure.Quartz.Jobs;
using Infrastructure.Quartz.Model;
using Infrastructure.Quartz.Scheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace MailSendAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
              
            })
            .ConfigureServices((hostContext, services) => 
            {
              services.AddSingleton<IJobFactory, MessageJobFactory>();
              services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
              services.AddSingleton<NotificationJob>();
              services.AddSingleton(new JobMetadata(Guid.NewGuid(),typeof(NotificationJob),"Notify job","0/10 * * * * ?"));

              services.AddHostedService<SchedulerMessage>();
            });
  }
}
