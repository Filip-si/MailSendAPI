using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface ITemplateService
  {
    Task<IEnumerable<TemplateResponse>> GetTemplates();

    Task<Guid> AddTemplate(FileRequest fileRequest, string from, string to);

    Task DeleteTemplate(Guid templateId);
  }
}
