using System;

namespace Domain.Entities
{
  public class Message
  {
    public Message(string From, string To, Guid MailMessageTemplateId, DateTime CreatedOn)
    {
      this.From = From;
      this.To = To;
      this.MailMessageTemplateId = MailMessageTemplateId;
      this.CreatedOn = CreatedOn;
    }

    public Guid MessageId { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid MailMessageTemplateId { get; set; }

    public virtual MailMessageTemplate MailMessageTemplate { get; set; }

    public virtual OutboxMessage OutboxMessage { get; set; }
  }
}
