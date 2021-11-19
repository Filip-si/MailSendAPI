using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class Files
  {

    public Guid? FilesId { get; set; }

    public Guid? FileHeaderId { get; set; }

    public virtual FileHeader FileHeader { get; set; }

    public virtual ICollection<FileAttachment> FilesAttachments { get; set; }

    public virtual Template Template { get; set; }

  }
}
