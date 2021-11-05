using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class MailMessage
  {
    public Guid MailMessageId { get; set; }

    public ICollection<string> Targets { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public Account Account { get; set; }
  }
}
