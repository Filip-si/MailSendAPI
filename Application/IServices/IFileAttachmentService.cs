using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileAttachmentService
  {
    Task<Guid?> SaveFileAttachment(FileAttachment fileAttachment);

    Task DeleteFileAttachment(Guid? fileAttachmentId);
  }
}
