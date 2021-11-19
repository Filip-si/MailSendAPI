using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileAttachmentService
  {
    Task<IEnumerable<FileAttachment>> GetFileAttachments();

    Task<Guid?> SaveFileAttachment(FileAttachment fileAttachment);

    Task DeleteFileAttachment(Guid? fileAttachmentId);
  }
}
