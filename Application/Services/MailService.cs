using Application.IServices;
using Application.Models;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
  public class MailService : IMailService
  {
    private readonly IConfiguration _configuration;
    private readonly DatabaseContext _context;

    public MailService(IConfiguration configuration, DatabaseContext context)
    {
      _configuration = configuration;
      _context = context;
    }
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
        await SmtpClientConfig().SendMailAsync(message);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task SendMailMessageByTemplate(Guid templateId, string recepients)
    {
      await _context.MailMessageTemplates.AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);

      await _context.Files.AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);

      var mailMessageTemplate = await _context.MailMessageTemplates.SingleAsync(x => x.MailMessageTemplateId == templateId);

      MailMessage mailMessage = new();
      mailMessage.From = new MailAddress(_configuration["EmailConfigurations:From"]);
      mailMessage.CC.Add(recepients);
      mailMessage.Subject = mailMessageTemplate.Subject;
      mailMessage.Body = mailMessageTemplate.Body;

      var attachments = await _context.Files
        .Where(x => x.MailMessageTemplateId == templateId)
        .ToListAsync();
      foreach (var file in attachments)
      {
        Stream stream = new MemoryStream(file.DataFiles);
        mailMessage.Attachments.Add(new Attachment(stream, file.FileName));
      }

      await SmtpClientConfig().SendMailAsync(mailMessage);
    }

    private SmtpClient SmtpClientConfig()
    {
      SmtpClient client = new(_configuration["EmailConfigurations:Host"], int.Parse(_configuration["EmailConfigurations:Port"]));
      NetworkCredential creds = new(_configuration["EmailConfigurations:From"], _configuration["EmailConfigurations:Password"]);
      client.EnableSsl = bool.Parse(_configuration["EmailConfigurations:EnableSsl"]);
      client.UseDefaultCredentials = bool.Parse(_configuration["EmailConfigurations:UseDefaultCredentials"]);
      client.DeliveryMethod = SmtpDeliveryMethod.Network;
      client.Credentials = creds;
      return client;
    }
  }
}
