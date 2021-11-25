using System;

namespace Application.Models
{
  public class FileAttachmentResponse
  {
    public Guid? FileAttachmentId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }
  }
}
