using Autofac;
using Serilog;

namespace Infrastructure.Logger
{
  public class LoggerAutofacModule : Module
  {
    private readonly ILogger _logger;

    public LoggerAutofacModule(ILogger logger)
    {
      _logger = logger;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterInstance(_logger)
        .As<ILogger>()
        .SingleInstance();
    }
  }
}
