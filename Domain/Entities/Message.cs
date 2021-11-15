using System;

namespace Domain.Entities
{
  public class Message
  {
    public Message(string From, string To, Guid MailMessageTemplateId, DateTime CreatedOn, bool IsSent)
    {
      this.From = From;
      this.To = To;
      this.MailMessageTemplateId = MailMessageTemplateId;
      this.CreatedOn = CreatedOn;
      this.IsSent = IsSent;
    }

    public Guid MessageId { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public bool IsSent { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid MailMessageTemplateId { get; set; }

    public virtual MailMessageTemplate MailMessageTemplate { get; set; }

  }
}
