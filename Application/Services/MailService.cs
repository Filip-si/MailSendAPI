﻿using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    private readonly IMessageService _messageService;

    public MailService(IConfiguration configuration, DatabaseContext context, IMessageService messageService)
    {
      _configuration = configuration;
      _context = context;
      _messageService = messageService;
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

    public async Task SendMailMessageByTemplate(Guid templateId, IEnumerable<string> recepients)
    {
      try
      {
        await _context.MailMessageTemplates.AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);

        var mailMessageTemplate = await _context.MailMessageTemplates
          .Include(x => x.Files)
          .SingleAsync(x => x.MailMessageTemplateId == templateId);

        var messages = await _messageService.SaveMessagesAsync(recepients, templateId);

        var mailMessages = await CreateMailMessagesAsync(messages);

        foreach (var mail in mailMessages)
        {
          await SmtpClientConfig().SendMailAsync(mail);
        }
      }
      catch (Exception)
      {
        throw;
      }
    }

    private async Task<List<MailMessage>> CreateMailMessagesAsync(List<OutboxMessage> outboxMessages)
    {
      List<MailMessage> mailMessages = new();
      foreach (var outbox in outboxMessages)
      {
        var message = _context.Messages.Where(x => x.MessageId == outbox.MessageId).Single();
        MailMessage mail = new(_configuration["EmailConfigurations:From"], message.To, message.MailMessageTemplate.Subject, message.MailMessageTemplate.Body);
        await AddAttachmentsAsync(message.MailMessageTemplateId, mail);

        mailMessages.Add(mail);
      }

      return mailMessages;
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
