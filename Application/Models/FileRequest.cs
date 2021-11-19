using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Models
{
  public class FileRequest
  {
    public IFormFile FileHeader { get; set; }
    public IEnumerable<IFormFile> FileAttachments { get; set; }
  }
}
