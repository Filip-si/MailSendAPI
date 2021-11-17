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

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> SendMailMessage(MailRequest request)
    {
      await _mailService.SendMailMessage(request);
      return StatusCode(StatusCodes.Status202Accepted);
    }

    [HttpPost("{templateId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> SendMailMessageByTemplate(Guid templateId,[FromForm] [BindRequired] IEnumerable<string> recepients)
    {
      await _mailService.SendMailMessageByTemplate(templateId, recepients);
      return StatusCode(StatusCodes.Status202Accepted);
    }
  }
}
