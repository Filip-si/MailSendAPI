using System;
using System.Collections.Generic;

namespace Application.Models.MailMessage
{
  public class MailMessageRequest
  {
    public ICollection<string> Targets { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public Guid AccountId { get; set; }
  }
}
