using Application.IServices;
using Application.Models;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
      MailMessageTemplate mail = new()
      {
        Subject = template.Subject,
        Body = template.Body
      };

      await _context.AddAsync(mail);
      await _context.SaveChangesAsync();

      return mail.MailMessageTemplateId;    
    }

    public async Task DeleteMailMessageTemplate(Guid mailMessageTemplateId)
    {
      await _context.MailMessageTemplates
        .AsNoTracking()
        .IsAnyRuleAsync(x => x.MailMessageTemplateId == mailMessageTemplateId);

      var mailMessageTemplate = await _context.MailMessageTemplates.SingleAsync(x => x.MailMessageTemplateId == mailMessageTemplateId);

      _context.Remove(mailMessageTemplate);
      await _context.SaveChangesAsync();
    }
  }
}
