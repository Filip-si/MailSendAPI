using Application.IServices;
using Application.Models.Templates;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class MailService : IMailService
  {
    private readonly IFluentEmail _fluentEmail;
    private readonly DatabaseContext _context;

    public MailService(IFluentEmail fluentEmail, DatabaseContext context)
    {
      _fluentEmail = fluentEmail;
      _context = context;
    }

    public async Task SendEmailTemplate(Guid templateId, ICollection<string> recepients)
    {
      await _context.Templates.AsNoTracking()
        .IsAnyRuleAsync(x => x.TemplateId == templateId);

      var template = await _context.Templates.AsNoTracking()
        .Include(x => x.Files)
        .SingleAsync(x => x.TemplateId == templateId);

      var files = template.Files;

      var headerFile = await _context.FileHeaders.AsNoTracking()
        .SingleAsync(x => x.FileHeaderId == files.FileHeaderId);

      var attachmentFiles = await _context.FileAttachments.AsNoTracking()
        .Where(x => x.FilesId == template.FilesId)
        .ToListAsync();

      foreach (var recepient in recepients)
      {
        var headerAttachment = GetImageInline(new MemoryStream(headerFile.DataFiles), headerFile.ContentType, headerFile.FileName);

        var attachments = new List<Attachment>();
        foreach (var attachment in attachmentFiles)
        {
          attachments.Add(GetAttachment(new MemoryStream(attachment.DataFiles), attachment.ContentType, attachment.FileName));
        }

        var email = _fluentEmail
          .To(recepient)
          .Subject("temat")
          .UsingTemplateFromFile(
          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/BasicTemplate.cshtml"),
          new BasicModel
          {
            Data = recepient,
            FileHeaderData = headerAttachment.ContentId
          })
          .Attach(attachments)
          .Attach(headerAttachment);

        await email.SendAsync();
      }
    }

    private static Attachment GetImageInline(Stream stream, string contentType, string fileName)
    {
      return new Attachment
      {
        Data = stream,
        ContentType = contentType,
        Filename = fileName,
        ContentId = Guid.NewGuid().ToString(),
        IsInline = true
      };
    }

    private static Attachment GetAttachment(Stream stream, string contentType, string fileName)
    {
      return new Attachment
      {
        Data = stream,
        ContentType = contentType,
        Filename = fileName,
        ContentId = Guid.NewGuid().ToString(),
        IsInline = false
      };
    }


  }
}
