using System;

namespace Domain.Entities
{
  public class FileAttachment
  {
    public FileAttachment()
    {

    }
    public FileAttachment(string fileName, string contentType, byte[] dataFiles)
    {
      FileName = fileName;
      ContentType = contentType;
      DataFiles = dataFiles;
    }

    public Guid? FileAttachmentId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public Guid? FilesId { get; set; }

    public virtual File Files { get; set; }
  }
}