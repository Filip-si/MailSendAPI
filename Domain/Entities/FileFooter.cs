using System;

namespace Domain.Entities
{
  public class FileFooter
  {
    public FileFooter()
    {

    }

    public FileFooter(string fileName, string contentType, byte[] dataFiles)
    {
      FileName = fileName;
      ContentType = contentType;
      DataFiles = dataFiles;
    }

    public Guid? FileFooterId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public virtual File Files { get; set; }
  }
}
