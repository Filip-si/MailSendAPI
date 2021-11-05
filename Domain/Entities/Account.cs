using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class Account
  {
    public Guid AccountId { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Host Host { get; set; }

    public ICollection<MailMessage> MailMessages { get; set; }
  }
}
