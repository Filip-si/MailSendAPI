using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileFooterService
  {
    Task<IEnumerable<FileFooter>> GetFileFooters();

    Task<Guid?> SaveFileFooter(FileFooter fileFooter);

    Task DeleteFileFooter(Guid? fileFooterId);
  }
}
