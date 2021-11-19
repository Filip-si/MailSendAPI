using Application.Models;
using Application.Models.Templates;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendMailMessage(MailRequest request);

    Task SendEmailFromTemplate(BasicModel templateModel, string recepient);
  }
}
