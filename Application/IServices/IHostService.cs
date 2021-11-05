using Application.Models.Host;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IHostService
  {
    Task AddSmtpHost(HostRequest request);

    Task DeleteSmtpHost(Guid hostId);
  }
}
