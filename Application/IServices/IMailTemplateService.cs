using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailTemplateService
  {
    Task<List<MailMessageTemplate>> GetMailMessageTemplates();

    Task<Guid> AddMailMessageTemplate(MailMessageTemplateRequest template);

    Task DeleteMailMessageTemplate(Guid mailMessageTemplateId);
  }
}
