using System;

namespace Application.Models
{
  public class TemplateResponse
  {
    public Guid TemplateId { get; set; }

    public string TextTemplate { get; set; }

    public string DataTemplate { get; set; }

    public Guid? FilesId { get; set; }
  }
}
