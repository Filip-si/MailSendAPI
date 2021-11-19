using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileHeaderService
  {
    Task<IEnumerable<FileHeader>> GetFileHeaders();

    Task<Guid?> SaveFileHeader(FileHeader fileHeader);

    Task DeleteFileHeader(Guid? fileHeaderId);
  }
}
