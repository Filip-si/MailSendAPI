using Application.Models;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IMailService
  {
    Task SendMailMessage(MailRequest request);
  }
}
