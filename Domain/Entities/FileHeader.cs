using System;

namespace Domain.Entities
{
  public class FileHeader
  {
    public FileHeader()
    {

    }

    public FileHeader(string fileName, string contentType, byte[] dataFiles)
    {
      FileName = fileName;
      ContentType = contentType;
      DataFiles = dataFiles;
    }

    public Guid? FileHeaderId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public virtual File Files { get; set; }

  }
}
