using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMessageService
  {
    Task<IEnumerable<Message>> GetMessages();

    Task<List<OutboxMessage>> SaveMessagesAsync(IEnumerable<string> recepients, Guid templateId);

    Task AddMessage(string recepients, Guid templateId);

    Task DeleteMessagesBeforeDate(DateTime dateTime);
  }
}
