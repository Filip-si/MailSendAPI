using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSendAPI.Controllers
{
  [ApiController]
  [Route("api/mails")]
  public class MailsController : ControllerBase
  {
    private readonly IMailService _mailService;

    public MailsController(IMailService mailService)
    {
      _mailService = mailService;
    }

    [HttpPost("html-template")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmailHtmlTemplate(Guid templateId, [BindRequired] string subject, [BindRequired] ICollection<RecepientRequest> recepients)
    {
      await _mailService.SendEmailHtmlTemplate(templateId, subject, recepients);
      return StatusCode(StatusCodes.Status201Created);
    }


    [HttpPost("newsletter-template")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmailTemplateNewsletter(Guid templateId,[BindRequired] string subject, [BindRequired] ICollection<RecepientRequest> recepients)
    {
      await _mailService.SendEmailTemplateNewsletter(templateId, subject, recepients);
      return StatusCode(StatusCodes.Status201Created);
    }
  }
}
