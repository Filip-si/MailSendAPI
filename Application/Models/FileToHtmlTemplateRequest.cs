using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Models
{
  public class FileToHtmlTemplateRequest
  {
    public IFormFile FileHeader { get; set; }
    public IFormFile FileBody { get; set; }
    public IFormFile FileFooter { get; set; }
    public IEnumerable<IFormFile> FileAttachments { get; set; }
  }
}
