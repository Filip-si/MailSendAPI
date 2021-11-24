using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileFooterService
  {
    Task<Guid?> SaveFileFooter(FileFooter fileFooter);

    Task DeleteFileFooter(Guid? fileFooterId);
  }
}
