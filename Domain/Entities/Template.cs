using System;

namespace Domain.Entities
{
  public class Template
  {
    public Template()
    {

    }
    public Template(string From, string To, Guid FilesId, DateTime CreatedOn)
    {
      this.From = From;
      this.To = To;
      this.FilesId = FilesId;
      this.CreatedOn = CreatedOn;
    }

    public Guid TemplateId { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public DateTime CreatedOn { get; set; }

    //public Guid MessageId { get; set; }

    //public virtual Message Message { get; set; }

    public Guid? FilesId { get; set; }

    public virtual Files Files{ get; set; }
  }
}
