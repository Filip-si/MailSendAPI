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

    public async Task SendEmailHtmlTemplate(Guid templateId, string subject, ICollection<RecipientRequest> recipients)
    {
      var template = await ReturnsTemplateIfExists(templateId);

      if (!template.FilesId.HasValue || 
        !template.Files.FileHeaderId.HasValue || 
        !template.Files.FileBodyId.HasValue || 
        !template.Files.FileFooterId.HasValue)
      {
        throw new BusinessException("Wrong templateId for set values","409");
      }

      var headerAttachment = await GetHeaderAttachment(template);
      var bodyAttachment = await GetBodyAttachment(template);
      var footerAttachment = await GetFooterAttachment(template);
      var attachments = await GetEmailAttachments(template);

      foreach (var recipient in recipients)
      {
        var email = _fluentEmail
          .To(recipient.Email)
          .Subject(subject)
          .UsingTemplateFromFile(
          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/BasicTemplate.cshtml"),
          new BasicModel
          {
            DataTemplate = template.DataTemplate,
            TextTemplate = template.TextTemplate,
            FirstName = recipient.FirstName,
            LastName = recipient.LastName,
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


    public async Task SendEmailTemplateNewsletter(Guid templateId, string subject, ICollection<RecipientRequest> recipients)
    {
      var template = await ReturnsTemplateIfExists(templateId);

      if (template.Files.FileHeaderId.HasValue ||
          await _context.FileAttachments.AnyAsync(x => x.FilesId == template.FilesId))
      {
        throw new BusinessException("Wrong templateId for set values", "409");
      }

      var bodyAttachment = await GetBodyAttachment(template);
      var footerAttachment = await GetFooterAttachment(template);

      foreach (var recipient in recipients)
      {

        var email = _fluentEmail
          .To(recipient.Email)
          .Subject(subject)
          .UsingTemplateFromFile(
          Path.Combine($"{Directory.GetCurrentDirectory()}/Templates/NewsletterTemplate.cshtml"),
          new NewsletterModel
          {
            FirstName = recipient.FirstName,
            LastName = recipient.LastName,
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

    private async Task<Attachment> GetHeaderAttachment(Template template)
    {
      var headerFile = await _context.FileHeaders.AsNoTracking()
          .SingleAsync(x => x.FileHeaderId == template.Files.FileHeaderId);
      return GetImageInline(new MemoryStream(headerFile.DataFiles), headerFile.ContentType, headerFile.FileName);
    }

    private async Task<Attachment> GetBodyAttachment(Template template)
    {
      var bodyFile = await _context.FileBodies.AsNoTracking()
          .SingleAsync(x => x.FileBodyId == template.Files.FileBodyId);
      return GetImageInline(new MemoryStream(bodyFile.DataFiles), bodyFile.ContentType, bodyFile.FileName);
    }

    private async Task<Attachment> GetFooterAttachment(Template template)
    {
      var footerFile = await _context.FileFooters.AsNoTracking()
          .SingleAsync(x => x.FileFooterId == template.Files.FileFooterId);
      return GetImageInline(new MemoryStream(footerFile.DataFiles), footerFile.ContentType, footerFile.FileName);
    }

    private async Task<List<Attachment>> GetEmailAttachments(Template template)
    {
      var attachmentsFiles = new List<FileAttachment>();
      if (await _context.FileAttachments.AnyAsync(x => x.FilesId == template.FilesId))
      {
        attachmentsFiles = await _context.FileAttachments.AsNoTracking()
          .Where(x => x.FilesId == template.FilesId)
          .ToListAsync();
      }

      var attachments = new List<Attachment>();
      foreach (var attachment in attachmentsFiles)
      {
        attachments.Add(GetAttachment(new MemoryStream(attachment.DataFiles), attachment.ContentType, attachment.FileName));
      }
      return attachments;
    }

    private async Task<Template> ReturnsTemplateIfExists(Guid templateId)
    {
      await _context.Templates.AsNoTracking()
        .IsAnyRuleAsync(x => x.TemplateId == templateId);

      return await _context.Templates.AsNoTracking()
        .Include(x => x.Files)
        .SingleAsync(x => x.TemplateId == templateId);
    }
  }
}
