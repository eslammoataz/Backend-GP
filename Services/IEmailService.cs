using WebApplication1.Models.Emails;

namespace WebApplication1.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto message);
    }
}