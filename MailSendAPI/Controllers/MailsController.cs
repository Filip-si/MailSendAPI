using Application.IServices;
using Application.Models;
using Application.Models.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public async Task<IActionResult> SendMailMessage(MailRequest request)
    {
      await _mailService.SendMailMessage(request);
      return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("template")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmailFromTemplate([FromForm] BasicModel basicModel, [BindRequired] string recepient)
    {
      await _mailService.SendEmailFromTemplate(basicModel, recepient);
      return StatusCode(StatusCodes.Status201Created);
    }
  }
}
