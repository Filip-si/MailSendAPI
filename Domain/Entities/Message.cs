using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class Message
  {
    public Guid MessageId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public virtual Template Template { get; set; }



  }
}
