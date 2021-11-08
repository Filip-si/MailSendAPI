﻿using Application.Models;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendMailMessage(MailRequest request);


    Task SendMailMessageByTemplate(Guid mailMessageTemplateId, string recepients);
  }
}
