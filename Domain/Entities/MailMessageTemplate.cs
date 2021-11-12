using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class MailMessageTemplate
  {
    public Guid MailMessageTemplateId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public virtual ICollection<File> Files { get; set; }
  }
}
