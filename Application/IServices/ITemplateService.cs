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

    Task<Guid> AddTemplateHtml(FileToHtmlTemplateRequest fileRequest, DataRequest dataRequest);

    Task<Guid> AddTemplateNewsletter(FileToNewsletterTemplateRequest fileRequest);

    Task DeleteTemplate(Guid templateId);
  }
}
