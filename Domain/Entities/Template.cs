using System;

namespace Domain.Entities
{
  public class Template
  {
    public Template()
    {

    }
    public Template(string TextTemplate, string DataTemplate, Guid FilesId, DateTime CreatedOn)
    {
      this.TextTemplate = TextTemplate;
      this.DataTemplate = DataTemplate;
      this.FilesId = FilesId;
      this.CreatedOn = CreatedOn;
    }

    public Guid TemplateId { get; set; }

    public string TextTemplate { get; set; }

    public string DataTemplate { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? FilesId { get; set; }

    public virtual Files Files{ get; set; }
  }
}
