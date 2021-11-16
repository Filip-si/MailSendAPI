using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class MailMessageTemplate
  {
    public MailMessageTemplate(string Subject, string Body)
    {
      this.Subject = Subject;
      this.Body = Body;
    }

    public Guid MailMessageTemplateId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public virtual ICollection<File> Files { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
  }
}
