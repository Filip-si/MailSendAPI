using Microsoft.AspNetCore.Http;

namespace Application.Models
{
  public class FileNewsletterRequest
  {
    public IFormFile FileBody { get; set; }
    public IFormFile FileFooter { get; set; }
  }
}
