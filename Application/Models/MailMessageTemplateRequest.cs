using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Models
{
  public class MailMessageTemplateRequest
  {
    public string Subject { get; set; }

    public string Body { get; set; }

    public IEnumerable<IFormFile> Files { get; set; }
  }
}
