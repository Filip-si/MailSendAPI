using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class MailTemplateService : IMailTemplateService
  {
    private readonly DatabaseContext _context;
    private readonly IFileService _fileService;

    public MailTemplateService(DatabaseContext context, IFileService fileService)
    {
      _context = context;
      _fileService = fileService;
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
        MailMessageTemplate mail = new()
        {
          Subject = template.Subject,
          Body = template.Body
        };

        await _context.AddAsync(mail);
        await _context.SaveChangesAsync();

        foreach (var file in template.Files)
        {
          await _fileService.UploadFiles(file, mail.MailMessageTemplateId);
        }

        await transaction.CommitAsync();
        return mail.MailMessageTemplateId;
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }

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
