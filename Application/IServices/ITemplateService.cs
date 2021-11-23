using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface ITemplateService
  {
    Task<IEnumerable<TemplateResponse>> GetTemplates();

    Task<IEnumerable<TemplateResponse>> GetTemplatesHtml();

    Task<IEnumerable<TemplateResponse>> GetTemplatesNewsletter();

    Task<Guid> AddTemplateHtml(FileRequest fileRequest, DataRequest dataRequest);

    Task<Guid> AddTemplateNewsletter(FileNewsletterRequest fileRequest);

    Task DeleteTemplate(Guid templateId);
  }
}
