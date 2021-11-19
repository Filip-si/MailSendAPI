//using Application.IServices;
//using Application.Models;
//using Application.Models.Templates;
//using Domain.Entities;
//using FluentEmail.Core;
//using Infrastructure;
//using Infrastructure.Extensions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//  public class MailService : IMailService
//  {
//    private readonly IFluentEmail _fluentEmail;
//    private readonly IConfiguration _configuration;
//    private readonly DatabaseContext _context;
//    private readonly IMessageService _messageService;

//    public MailService(IFluentEmail fluentEmail, IConfiguration configuration, DatabaseContext context, IMessageService messageService)
//    {
//      _fluentEmail = fluentEmail;
//      _configuration = configuration;
//      _context = context;
//      _messageService = messageService;
//    }

//    public async Task SendMailMessage(MailRequest request)
//    {
//      try
//      {
//        MailMessage message = new(
//          request.From,
//          request.To,
//          request.Subject,
//          request.Body);
//        message.BodyEncoding = Encoding.UTF8;
//        message.IsBodyHtml = true;
//        await SmtpClientConfig().SendMailAsync(message);
//      }
//      catch (Exception)
//      {
//        throw;
//      }
//    }

//    public async Task SendEmailFromTemplate(BasicModel templateModel, string recepient)
//    {
//      var file = templateModel.File;

//      var email = _fluentEmail
//          .To(recepient)
//          .Subject("temat")
//          .UsingTemplateFromFile(
//          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/BasicTemplate.cshtml"),
//          new BasicModel
//          {
//            Data = templateModel.Data,
//            File = templateModel.File
//          });
//      var stream = System.IO.File.OpenRead(file.FileName);
//      email.Attach(new FluentEmail.Core.Models.Attachment()
//      {
//        Filename = file.FileName,
//        ContentType = file.ContentType,
//        Data = stream,
//        IsInline = true
//      });
//      await email.SendAsync();
//    }

//    private SmtpClient SmtpClientConfig()
//    {
//      SmtpClient client = new(_configuration["EmailConfigurations:Host"], int.Parse(_configuration["EmailConfigurations:Port"]));
//      NetworkCredential creds = new(_configuration["EmailConfigurations:From"], _configuration["EmailConfigurations:Password"]);
//      client.EnableSsl = bool.Parse(_configuration["EmailConfigurations:EnableSsl"]);
//      client.UseDefaultCredentials = bool.Parse(_configuration["EmailConfigurations:UseDefaultCredentials"]);
//      client.DeliveryMethod = SmtpDeliveryMethod.Network;
//      client.Credentials = creds;
//      return client;
//    }


//  }
//}
