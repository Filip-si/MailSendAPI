using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class File
  {

    public Guid? FilesId { get; set; }

    public Guid? FileHeaderId { get; set; }

    public virtual FileHeader FileHeader { get; set; }

    public Guid? FileBodyId { get; set; }

    public virtual FileBody FileBody { get; set; }

    public Guid? FileFooterId { get; set; }

    public virtual FileFooter FileFooter { get; set; }

    public virtual ICollection<FileAttachment> FilesAttachments { get; set; }

    public virtual Template Template { get; set; }

  }
}
