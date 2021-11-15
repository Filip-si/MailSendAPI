using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendMailMessage(MailRequest request);

    Task SendMailMessageByTemplate(Guid templateId, IEnumerable<string> recepients);
  }
}
