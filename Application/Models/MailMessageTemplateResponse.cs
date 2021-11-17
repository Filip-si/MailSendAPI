using System;
using System.Collections.Generic;

namespace Application.Models
{
  public class MailMessageTemplateResponse
  {
    public Guid MailTemplateId { get; set; }
    public string Subject { get; set; }

    public string Body { get; set; }

    public IEnumerable<Guid> Files { get; set; }

    public IEnumerable<Guid> Messages { get; set; }
  }
}
