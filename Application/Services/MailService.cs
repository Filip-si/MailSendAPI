﻿using Application.IServices;
using Application.Models;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        await GetSmtpClientConfig().SendMailAsync(message);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task SendMailMessageByTemplate(Guid mailMessageTemplateId, string recepients)
    {
      try
      {
        await _context.MailMessageTemplates.AsNoTracking()
          .IsAnyRuleAsync(x => x.MailMessageTemplateId == mailMessageTemplateId);

        var mailMessageTemplate = await _context.MailMessageTemplates.SingleAsync(x => x.MailMessageTemplateId == mailMessageTemplateId);

        MailMessage mailMessage = new();
        mailMessage.From = new MailAddress(_configuration["EmailConfigurations:From"]);
        mailMessage.CC.Add(recepients);
        mailMessage.Subject = mailMessageTemplate.Subject;
        mailMessage.Body = mailMessageTemplate.Body;
        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure; // need?
        await GetSmtpClientConfig().SendMailAsync(mailMessage);
      }
      catch (Exception)
      {
        throw;
      }
    }

    private SmtpClient GetSmtpClientConfig()
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
