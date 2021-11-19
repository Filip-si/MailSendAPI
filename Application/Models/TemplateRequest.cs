using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
  public class TemplateRequest
  {
    public IFormFile FileHeader { get; set; }
    public IEnumerable<IFormFile> FileAttachments { get; set; }
  }
}
