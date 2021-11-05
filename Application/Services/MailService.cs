using Application.IServices;
using Application.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
  public class MailService : IMailService
  {
    public async Task SendMailMessage(MailRequest request)
    {
      try
      {
        MailMessage message = new(
          request.From,
          request.To,
          request.Subject,
          request.Body);
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;

        SmtpClient client = new(request.Host, request.Port);
        NetworkCredential creds = new(request.From, request.Password);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = creds;
        await client.SendMailAsync(message);
      }
      catch (Exception)
      {
        throw;
      }
    }

  }
}
