using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMessageService
  {
    Task<IEnumerable<Message>> GetMessages();

    Task AddMessages(IEnumerable<string> recepients, Guid templateId);

    Task AddMessage(string recepients, Guid templateId);

    Task DeleteMessagesBeforeDate(DateTime dateTime);
  }
}
