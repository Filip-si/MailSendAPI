using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendEmailHtmlTemplate(Guid templateId, string subject, ICollection<RecepientRequest> recepients);

    Task SendEmailTemplateNewsletter(Guid templateId, string subject, ICollection<RecepientRequest> recepients);
  }
}
