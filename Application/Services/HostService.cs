using Application.IServices;
using Application.Models.Host;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
  public class HostService : IHostService
  {
    public Task AddSmtpHost(HostRequest request)
    {
      throw new NotImplementedException();
    }

    public Task DeleteSmtpHost(Guid hostId)
    {
      throw new NotImplementedException();
    }
  }
}
