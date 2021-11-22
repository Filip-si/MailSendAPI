using System;

namespace Domain.Entities
{
  public class FileBody
  {
    public FileBody()
    {

    }

    public FileBody(string FileName, string ContentType, byte[] DataFiles)
    {
      this.FileName = FileName;
      this.ContentType = ContentType;
      this.DataFiles = DataFiles;
    }

    public Guid? FileBodyId { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] DataFiles { get; set; }

    public virtual Files Files { get; set; }
  }
}
