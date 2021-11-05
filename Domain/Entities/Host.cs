using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class Host
  {
    public Guid HostId { get; set; }

    public string HostName { get; set; }

    public int Port { get; set; }

    public ICollection<Account> Accounts { get; set; }
  }
}
