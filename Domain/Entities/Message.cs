using System;

namespace Domain.Entities
{
  public class Message
  {
    public Guid MessageId { get; set; }

    public string From { get; set; }

    public string CC { get; set; }

    public bool SendingStatus { get; set; }

    public Guid MailMessageTemplateId { get; set; }

    public virtual MailMessageTemplate MailMessageTemplate { get; set; }

    public Message(string From, string CC, Guid MailMessageTemplateId, bool SendingStatus)
    {
      this.From = From;
      this.CC = CC;
      this.MailMessageTemplateId = MailMessageTemplateId;
      this.SendingStatus = SendingStatus;
    }
  }
}
