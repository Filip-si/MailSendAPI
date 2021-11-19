using Microsoft.AspNetCore.Http;

namespace Application.Models.Templates
{
  public class BasicModel
  {
    public string Data { get; set; }
    public IFormFile File { get; set; }
  }
}
