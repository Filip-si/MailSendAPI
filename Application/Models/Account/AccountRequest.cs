using System;

namespace Application.Models.Account
{
  public class AccountRequest
  {
    public string Email { get; set; }

    public string Password { get; set; }

    public Guid HostId { get; set; }
  }
}
