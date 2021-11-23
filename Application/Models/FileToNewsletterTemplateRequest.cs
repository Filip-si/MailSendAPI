using Microsoft.AspNetCore.Http;

namespace Application.Models
{
  public class FileToNewsletterTemplateRequest
  {
    public IFormFile FileBody { get; set; }
    public IFormFile FileFooter { get; set; }
  }
}
