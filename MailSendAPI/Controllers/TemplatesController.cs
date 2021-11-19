using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSendAPI.Controllers
{
  [ApiController]
  [Route("api/templates")]
  public class TemplateController : ControllerBase
  {
    private readonly ITemplateService _templateService;

    public TemplateController(ITemplateService templateService)
    {
      _templateService = templateService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(IEnumerable<MailMessageTemplateResponse>))]
    public async Task<IActionResult> GetTemplates()
    {
      return Ok(await _templateService.GetTemplates());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType(typeof(Guid))]
    public async Task<IActionResult> AddTemplate([FromForm] FileRequest fileRequest, string from, string to)
    {
      var templateId = await _templateService.AddTemplate(fileRequest, from, to);
      return StatusCode(StatusCodes.Status201Created, templateId);
    }

    [HttpDelete("{templateId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTemplate(Guid templateId)
    {
      await _templateService.DeleteTemplate(templateId);
      return StatusCode(StatusCodes.Status204NoContent);
    }

  }
}
