using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Models
{
  public class FilesResponse
  {
    public Guid? FilesId { get; set; }

    public Guid? FileHeaderId { get; set; }

    public Guid? FileBodyId { get; set; }

    public Guid? FileFooterId { get; set; }

    public ICollection<FileAttachmentResponse> FileAttachment { get; set; }
  }
}
