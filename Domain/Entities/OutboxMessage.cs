using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class OutboxMessage
  {
    public OutboxMessage(Guid OutboxMessageId, int Count)
    {
      this.OutboxMessageId = OutboxMessageId;
      this.Count = Count;
    }

    public Guid OutboxMessageId { get; set; }

    public int Count { get; set; }

    public Guid MessageId { get; set; }

    public virtual Message Message { get; set; }
  }
}
