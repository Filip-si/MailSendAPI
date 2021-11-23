using System;

namespace Domain.Entities
{
  public class FileAttachment
  {
    public FileAttachment()
    {

    }
    public FileAttachment(string FileName, string ContentType, byte[] DataFiles)
    {
      this.FileName = FileName;
      this.ContentType = ContentType;
      this.DataFiles = DataFiles;
    }

    public Guid? FileAttachmentId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public Guid? FilesId { get; set; }

    public virtual Files Files { get; set; }
  }
}