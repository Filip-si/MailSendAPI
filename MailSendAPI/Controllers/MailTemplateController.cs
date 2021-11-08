﻿using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public async Task<IActionResult> GetMailMessageTemplates()
    {
      await _mailTemplateService.GetMailMessageTemplates();
      return StatusCode(StatusCodes.Status200OK);
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