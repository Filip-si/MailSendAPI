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
  [Route("api/templates")]
  public class TemplatesController : ControllerBase
  {
    private readonly ITemplateService _templateService;

    public TemplatesController(ITemplateService templateService)
    {
      _templateService = templateService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(IEnumerable<TemplateResponse>))]
    public async Task<IActionResult> GetTemplates()
    {
      return Ok(await _templateService.GetTemplates());
    }

    [HttpGet("html-template")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(IEnumerable<TemplateResponse>))]
    public async Task<IActionResult> GetTemplatesHtml()
    {
      return Ok(await _templateService.GetTemplatesHtml());
    }

    [HttpGet("newsletter-template")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType(typeof(IEnumerable<TemplateResponse>))]
    public async Task<IActionResult> GetTemplatesNewsletter()
    {
      return Ok(await _templateService.GetTemplatesNewsletter());
    }

    [HttpPost("html-template")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType(typeof(Guid))]
    public async Task<IActionResult> AddTemplateHtml([FromQuery] DataRequest dataRequest, [FromForm] FileToHtmlTemplateRequest fileRequest)
    {
      var templateId = await _templateService.AddTemplateHtml(fileRequest, dataRequest);
      return StatusCode(StatusCodes.Status201Created, templateId);
    }

    [HttpPost("newsletter-template")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType(typeof(Guid))]
    public async Task<IActionResult> AddTemplateNewsletter([FromForm] FileToNewsletterTemplateRequest fileNewsletterRequest)
    {
      var templateId = await _templateService.AddTemplateNewsletter(fileNewsletterRequest);
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
