using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileBodyService
  {
    Task<IEnumerable<FileBody>> GetFileBodies();

    Task<Guid?> SaveFileBody(FileBody fileBody);

    Task DeleteFileBody(Guid? fileBodyId);
  }
}
