using Microsoft.AspNetCore.Http;

namespace Application.Models
{
  public class FileRequest
  {
    public IFormFile  File { get; set; }
  }
}
