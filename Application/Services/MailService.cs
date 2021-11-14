using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Exceptions;
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
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _context.MailMessageTemplates.AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);

        var mailMessageTemplate = await _context.MailMessageTemplates
          .Include(x => x.Files)
          .SingleAsync(x => x.MailMessageTemplateId == templateId);

        var mailMessage = CreateMailMessage(mailMessageTemplate, recepients);
        
        
        //await _context.AddAsync(mailMessage); 
        //var outbox = new OutboxMessageRequest
        //{
        //  Count = 0
        //};
        //await _context.AddAsync(outbox);
        //await _context.SaveChangesAsync();
        //await transaction.CommitAsync();

        // var outboxList = await _context.Outboxs.Where(x => x.Count == 0).ToListAsync();
        await SmtpClientConfig().SendMailAsync(mailMessage.Result);
        await transaction.CommitAsync();

      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }

    private async Task<MailMessage> CreateMailMessage(MailMessageTemplate mailMessageTemplate, string recepients)
    {
      MailMessage mailMessage = new();
      mailMessage.From = new MailAddress(_configuration["EmailConfigurations:From"]);
      mailMessage.CC.Add(recepients);
      mailMessage.Subject = mailMessageTemplate.Subject;
      mailMessage.Body = mailMessageTemplate.Body;
      await AddAttachmentsAsync(mailMessageTemplate.MailMessageTemplateId, mailMessage);
      return mailMessage;
    }

    private async Task AddAttachmentsAsync(Guid templateId, MailMessage mailMessage)
    {
      var attachments = await _context.Files
          .Where(x => x.MailMessageTemplateId == templateId)
          .ToListAsync();
      foreach (var file in attachments)
      {
        Stream stream = new MemoryStream(file.DataFiles);
        mailMessage.Attachments.Add(new Attachment(stream, file.FileName));
      }
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
