using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Infrastructure.Logger
{
  public static class LoggerConfigurationService
  {
    public static ILogger CreateLogger(IConfiguration configuration)
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .ReadFrom
        .Configuration(configuration)
        .CreateLogger();
      return Log.Logger;
    }
  }
}
