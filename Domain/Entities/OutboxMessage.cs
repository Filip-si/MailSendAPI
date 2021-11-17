using System;

namespace Domain.Entities
{
  public class OutboxMessage
  {
    public OutboxMessage(int Count)
    {
      this.Count = Count;
    }

    public Guid OutboxMessageId { get; set; }

    public int Count { get; set; }

    public Guid MessageId { get; set; }

    public virtual Message Message { get; set; }
  }
}
