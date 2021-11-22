using Application.IServices;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmailTemplate(Guid templateId, [BindRequired] ICollection<string> recepient)
    {
      await _mailService.SendEmailTemplate(templateId, recepient);
      return StatusCode(StatusCodes.Status201Created);
    }
  }
}
