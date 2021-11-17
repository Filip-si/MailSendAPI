using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailTemplateService
  {
    Task<IEnumerable<MailMessageTemplateResponse>> GetMailMessageTemplates();

    Task<Guid> AddMailMessageTemplate(MailMessageTemplateRequest template);

    Task<File> UploadFilesToTemplate(IFormFile request, Guid templateId);

    Task DeleteMailMessageTemplate(Guid templateId);
  }
}
