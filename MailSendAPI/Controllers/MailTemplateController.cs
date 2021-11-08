using Application.IServices;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSendAPI.Controllers
{
  [ApiController]
  [Route("api/templates")]
  public class MailTemplateController : ControllerBase
  {
    private readonly IMailTemplateService _mailTemplateService;

    public MailTemplateController(IMailTemplateService mailTemplateService)
    {
      _mailTemplateService = mailTemplateService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(IEnumerable<MailMessageTemplate>))]
    public async Task<IActionResult> GetMailMessageTemplates()
    {      
      return Ok(await _mailTemplateService.GetMailMessageTemplates());
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType(typeof(Guid))]
    public async Task<IActionResult> AddMailMessageTemplate(MailMessageTemplateRequest template)
    {
      var mailMessageTemplateId = await _mailTemplateService.AddMailMessageTemplate(template);
      return StatusCode(StatusCodes.Status201Created, mailMessageTemplateId);
    }

    [HttpDelete("{mailMessageTemplateId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteMailMessageTemplate(Guid mailMessageTemplateId)
    {
      await _mailTemplateService.DeleteMailMessageTemplate(mailMessageTemplateId);
      return StatusCode(StatusCodes.Status204NoContent);
    }

  }
}
