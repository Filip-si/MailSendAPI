using System;

namespace Domain.Entities
{
  public class FileFooter
  {
    public FileFooter()
    {

    }

    public FileFooter(string FileName, string ContentType, byte[] DataFiles)
    {
      this.FileName = FileName;
      this.ContentType = ContentType;
      this.DataFiles = DataFiles;
    }

    public Guid? FileFooterId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public virtual Files Files { get; set; }
  }
}
