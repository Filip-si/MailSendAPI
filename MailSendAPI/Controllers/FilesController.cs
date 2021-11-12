using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MailSendAPI.Controllers
{
  [ApiController]
  [Route("api/files")]
  public class FilesController :  ControllerBase
  {
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
      _fileService = fileService;
    }

    [HttpPost("{templateId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType(typeof(Guid))]
    public async Task<IActionResult> UploadFiles(IFormFile request, Guid templateId)
    {
      await _fileService.UploadFiles(request, templateId);
      return StatusCode(StatusCodes.Status201Created);
    }
  }
}
