using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileService
  {
    Task<IEnumerable<Files>> GetFiles();

    Task<Guid?> SaveFilesAsync(Files files);

    Task DeleteFiles(Guid? filesId);
  }
}
