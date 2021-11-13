﻿using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailTemplateService
  {
    Task<IEnumerable<MailMessageTemplate>> GetMailMessageTemplates();

    Task<Guid> AddMailMessageTemplate(MailMessageTemplateRequest template);

    Task<File> UploadFilesToTemplate(FileRequest request, Guid templateId);

    Task DeleteMailMessageTemplate(Guid templateId);
  }
}
