using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace MailSendAPI.Configurations
{
  public static class EmailConfigurationExtension
  {
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
      var email = configuration.GetSection("EmailConfigurations").Get<EmailConfigurationModel>();

      services
        .AddFluentEmail(email.Sender, email.From)
        .AddRazorRenderer()
        .AddSmtpSender(new SmtpClient(email.Host)
        {
          UseDefaultCredentials = false,
          Port = email.Port,
          Credentials = new NetworkCredential(email.Sender, email.Password),
          EnableSsl = true
        });

      return services;
    }
  }
}
