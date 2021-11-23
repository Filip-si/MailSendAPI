using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendEmailHtmlTemplate(Guid templateId, ICollection<RecepientRequest> recepients);

    Task SendEmailTemplateNewsletter(Guid templateId, ICollection<RecepientRequest> recepients);
  }
}
