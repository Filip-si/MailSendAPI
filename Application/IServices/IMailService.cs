using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendEmailTemplate(Guid templateId, ICollection<string> recepients);
  }
}
