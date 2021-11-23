using Application.IServices;
using Application.Models;
using Application.Models.Templates;
using Domain.Entities;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Infrastructure;
using Infrastructure.Exceptions;
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

    public async Task SendEmailHtmlTemplate(Guid templateId, ICollection<RecepientRequest> recepients)
    {
      await _context.Templates.AsNoTracking()
        .IsAnyRuleAsync(x => x.TemplateId == templateId);

      var template = await _context.Templates.AsNoTracking()
        .Include(x => x.Files)
        .SingleAsync(x => x.TemplateId == templateId);

      if(!template.FilesId.HasValue || 
        !template.Files.FileHeaderId.HasValue || 
        !template.Files.FileBodyId.HasValue || 
        !template.Files.FileFooterId.HasValue)
      {
        throw new BusinessException("Wrong templateId for set values","409");
      }
      
      var headerFile = await _context.FileHeaders.AsNoTracking()
          .SingleAsync(x => x.FileHeaderId == template.Files.FileHeaderId);
      var headerAttachment = GetImageInline(new MemoryStream(headerFile.DataFiles), headerFile.ContentType, headerFile.FileName);
     
      var bodyFile = await _context.FileBodies.AsNoTracking()
          .SingleAsync(x => x.FileBodyId == template.Files.FileBodyId);
      var bodyAttachment = GetImageInline(new MemoryStream(bodyFile.DataFiles), bodyFile.ContentType, bodyFile.FileName);
      
      var footerFile = await _context.FileFooters.AsNoTracking()
          .SingleAsync(x => x.FileFooterId == template.Files.FileFooterId);
      var footerAttachment = GetImageInline(new MemoryStream(footerFile.DataFiles), footerFile.ContentType, footerFile.FileName);

      var attachmentsFiles = new List<FileAttachment>();
      if(await _context.FileAttachments.AnyAsync(x => x.FilesId == template.FilesId))
      {
        attachmentsFiles = await _context.FileAttachments.AsNoTracking()
          .Where(x => x.FilesId == template.FilesId)
          .ToListAsync();
      }

      foreach (var recepient in recepients)
      {
        var attachments = new List<Attachment>();
        foreach (var attachment in attachmentsFiles)
        {
          attachments.Add(GetAttachment(new MemoryStream(attachment.DataFiles), attachment.ContentType, attachment.FileName));
        }

        var email = _fluentEmail
          .To(recepient.Email)
          .Subject("temat")
          .UsingTemplateFromFile(
          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/BasicTemplate.cshtml"),
          new BasicModel
          {
            DataTemplate = template.DataTemplate,
            TextTemplate = template.TextTemplate,
            FirstName = recepient.FirstName,
            LastName = recepient.LastName,
            FileHeaderData = headerAttachment.ContentId,
            FileBodyData = bodyAttachment.ContentId,
            FileFooterData = footerAttachment.ContentId
          })
          .Attach(attachments)
          .Attach(headerAttachment)
          .Attach(bodyAttachment)
          .Attach(footerAttachment);

        await email.SendAsync();
      }
    }


    public async Task SendEmailTemplateNewsletter(Guid templateId, ICollection<RecepientRequest> recepients)
    {
      await _context.Templates.AsNoTracking()
        .IsAnyRuleAsync(x => x.TemplateId == templateId);

      var template = await _context.Templates.AsNoTracking()
        .Include(x => x.Files)
        .SingleAsync(x => x.TemplateId == templateId);

      if (template.Files.FileHeaderId.HasValue ||
          await _context.FileAttachments.AnyAsync(x => x.FilesId == template.FilesId))
      {
        throw new BusinessException("Wrong templateId for set values", "409");
      }

      var bodyAttachment = new Attachment();
      if (template.Files.FileBodyId.HasValue)
      {
        var bodyFile = await _context.FileBodies.AsNoTracking()
          .SingleAsync(x => x.FileBodyId == template.Files.FileBodyId);
        bodyAttachment = GetImageInline(new MemoryStream(bodyFile.DataFiles), bodyFile.ContentType, bodyFile.FileName);
      }

      var footerAttachment = new Attachment();
      if (template.Files.FileFooterId.HasValue)
      {
        var footerFile = await _context.FileFooters.AsNoTracking()
          .SingleAsync(x => x.FileFooterId == template.Files.FileFooterId);
        footerAttachment = GetImageInline(new MemoryStream(footerFile.DataFiles), footerFile.ContentType, footerFile.FileName);
      }

      foreach (var recepient in recepients)
      {

        var email = _fluentEmail
          .To(recepient.Email)
          .Subject("newsletter")
          .UsingTemplateFromFile(
          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/NewsletterTemplate.cshtml"),
          new NewsletterModel
          {
            FirstName = recepient.FirstName,
            LastName = recepient.LastName,
            FileBodyData = bodyAttachment.ContentId,
            FileFooterData = footerAttachment.ContentId
          })
          .Attach(bodyAttachment)
          .Attach(footerAttachment);

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
