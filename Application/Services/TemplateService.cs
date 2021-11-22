using Application.IServices;
using Application.Models;
using Application.Models.Templates;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class TemplateService : ITemplateService
  {
    private readonly DatabaseContext _context;
    private readonly IFileService _fileService;

    public TemplateService(DatabaseContext context, IFileService fileService)
    {
      _context = context;
      _fileService = fileService;
    }

    public async Task<IEnumerable<TemplateResponse>> GetTemplates()
    {
      return await _context.Templates
        .Select(response => new TemplateResponse
        {
          TemplateId = response.TemplateId
        })
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<Guid> AddTemplate(FileRequest fileRequest, string from, string to)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        var newTemplate = new Template();
        await _context.AddAsync(newTemplate);

        var newFiles = new Files();
        await _context.AddAsync(newFiles);

        newTemplate.FilesId = newFiles.FilesId;

        var fileHeader = new FileHeader();
        if(fileRequest.FileHeader != null)
        {
          fileHeader = await UploadFileHeaderToTemplateAsync(fileRequest.FileHeader, newTemplate.TemplateId);
        }

        var fileBody = new FileBody();
        if (fileRequest.FileBody != null)
        {
          fileBody = await UploadFileBodyToTemplateAsync(fileRequest.FileBody, newTemplate.TemplateId);
        }

        var fileFooter = new FileFooter();
        if (fileRequest.FileFooter != null)
        {
          fileFooter = await UploadFileFooterToTemplateAsync(fileRequest.FileFooter, newTemplate.TemplateId);
        }

        newFiles.FileHeaderId = fileHeader.FileHeaderId;
        newFiles.FileBodyId = fileBody.FileBodyId;
        newFiles.FileFooterId = fileFooter.FileFooterId;

        var fileAttachments = new List<FileAttachment>();
        if (fileRequest.FileAttachments != null)
        {
          foreach (var attachement in fileRequest.FileAttachments)
          {
            var fileAttachment = await UploadFileAttachmentToTemplateAsync(attachement, newTemplate.FilesId, newTemplate.TemplateId);
            fileAttachments.Add(fileAttachment);
          }
        }

        newTemplate.From = from;
        newTemplate.To = to;

        await _context.SaveChangesAsync(); 
        await transaction.CommitAsync();
        return newTemplate.TemplateId;
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }

    private async Task<FileHeader> UploadFileHeaderToTemplateAsync(IFormFile request, Guid templateId)
    {
      var index = request.FileName.LastIndexOf("\\");
      var shortName = request.FileName[(index + 1)..];

      var fileHeader = new FileHeader
      {
        ContentType = request.ContentType,
        FileName = shortName,
      };

      using (var target = new MemoryStream())
      {
        request.CopyTo(target);
        fileHeader.DataFiles = target.ToArray();
      }
      _context.FileHeaders.Add(fileHeader);
      await _context.SaveChangesAsync();
      return fileHeader;
    }

    private async Task<FileBody> UploadFileBodyToTemplateAsync(IFormFile request, Guid templateId)
    {
      var index = request.FileName.LastIndexOf("\\");
      var shortName = request.FileName[(index + 1)..];

      var fileBody = new FileBody
      {
        ContentType = request.ContentType,
        FileName = shortName,
      };

      using (var target = new MemoryStream())
      {
        request.CopyTo(target);
        fileBody.DataFiles = target.ToArray();
      }
      _context.FileBodies.Add(fileBody);
      await _context.SaveChangesAsync();
      return fileBody;
    }

    private async Task<FileFooter> UploadFileFooterToTemplateAsync(IFormFile request, Guid templateId)
    {
      var index = request.FileName.LastIndexOf("\\");
      var shortName = request.FileName[(index + 1)..];

      var fileFooter = new FileFooter
      {
        ContentType = request.ContentType,
        FileName = shortName,
      };

      using (var target = new MemoryStream())
      {
        request.CopyTo(target);
        fileFooter.DataFiles = target.ToArray();
      }
      _context.FileFooters.Add(fileFooter);
      await _context.SaveChangesAsync();
      return fileFooter;
    }

    private async Task<FileAttachment> UploadFileAttachmentToTemplateAsync(IFormFile request, Guid? filesId, Guid templateId)
    {
      var index = request.FileName.LastIndexOf("\\");
      var shortName = request.FileName[(index + 1)..];

      var fileAttachment = new FileAttachment
      {
        ContentType = request.ContentType,
        FileName = shortName,
        FilesId = filesId
      };

      using (var target = new MemoryStream())
      {
        request.CopyTo(target);
        fileAttachment.DataFiles = target.ToArray();
      }
      _context.FileAttachments.Add(fileAttachment);
      await _context.SaveChangesAsync();
      return fileAttachment;
    }

    public async Task DeleteTemplate(Guid templateId)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _context.Templates
        .AsNoTracking()
        .IsAnyRuleAsync(x => x.TemplateId == templateId);

        var template = await _context.Templates.SingleAsync(x => x.TemplateId == templateId);
        var filesId = await _context.Files
          .Where(x => x.FilesId == template.FilesId)
          .Select(x => x.FilesId)
          .SingleAsync();

        await _fileService.DeleteFiles(filesId);
        
        _context.Remove(template);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }
  }
}
