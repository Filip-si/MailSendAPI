using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class FileHeader
  {
    public FileHeader()
    {

    }

    public FileHeader(string FileName, string ContentType, byte[] DataFiles)
    {
      this.FileName = FileName;
      this.ContentType = ContentType;
      this.DataFiles = DataFiles;
    }

    public Guid? FileHeaderId { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] DataFiles { get; set; }

    public virtual Files Files { get; set; }

  }
}
