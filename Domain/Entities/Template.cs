using System;

namespace Domain.Entities
{
  public class Template
  {
    public Template()
    {

    }
    public Template(string textTemplate, string dataTemplate, Guid filesId, DateTime createdOn)
    {
      TextTemplate = textTemplate;
      DataTemplate = dataTemplate;
      FilesId = filesId;
      CreatedOn = createdOn;
    }

    public Guid TemplateId { get; set; }

    public string TextTemplate { get; set; }

    public string DataTemplate { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? FilesId { get; set; }

    public virtual File Files{ get; set; }
  }
}
