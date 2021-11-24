using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileBodyService
  {
    Task<Guid?> SaveFileBody(FileBody fileBody);

    Task DeleteFileBody(Guid? fileBodyId);
  }
}
