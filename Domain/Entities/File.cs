﻿using System;

namespace Domain.Entities
{
  public class File
  {
    public Guid FileId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] DataFiles { get; set; }

    public Guid MailMessageTemplateId { get; set; }

    public virtual MailMessageTemplate MailMessageTemplate { get; set; }
  }
}
