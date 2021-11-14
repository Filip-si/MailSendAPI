using Application.IServices;
using Application.Models;
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
  public class MailTemplateService : IMailTemplateService
  {
    private readonly DatabaseContext _context;

    public MailTemplateService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<MailMessageTemplate>> GetMailMessageTemplates()
    {
      return await _context.MailMessageTemplates.AsNoTracking()
        .ToListAsync();
    }

    public async Task<Guid> AddMailMessageTemplate(MailMessageTemplateRequest template)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        MailMessageTemplate temp = new(template.Subject, template.Body);

        await _context.AddAsync(temp);
        await _context.SaveChangesAsync();

        if(template.Files != null)
        {
          foreach (var file in template.Files)
          {
            await UploadFilesToTemplate(file, temp.MailMessageTemplateId);
          }
        }
        
        await transaction.CommitAsync();
        return temp.MailMessageTemplateId;
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }

    public async Task<Domain.Entities.File> UploadFilesToTemplate(IFormFile request, Guid templateId)
    {
      await _context.MailMessageTemplates.AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);
      int index = request.FileName.LastIndexOf("\\");
      var shortName = request.FileName.Substring(index + 1);

      var newFile = new Domain.Entities.File
      {
        ContentType = request.ContentType,
        FileName = shortName,
        MailMessageTemplateId = templateId
      };

      using (var target = new MemoryStream())
      {
        request.CopyTo(target);
        newFile.DataFiles = target.ToArray();
      }
      _context.Files.Add(newFile);

      await _context.SaveChangesAsync();
      return newFile;
    }

    public async Task DeleteMailMessageTemplate(Guid templateId)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _context.MailMessageTemplates
        .AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == templateId);

        if(await _context.Files.AnyAsync(x => x.MailMessageTemplateId == templateId))
        {
          var listOfFiles = await _context.Files.Where(x => x.MailMessageTemplateId == templateId).ToListAsync();
          _context.RemoveRange(listOfFiles);
        }

        var mailMessageTemplate = await _context.MailMessageTemplates.SingleAsync(x => x.MailMessageTemplateId == templateId);

        _context.Remove(mailMessageTemplate);
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
