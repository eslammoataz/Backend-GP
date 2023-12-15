using WebApplication1.Models.Emails;

namespace WebApplication1.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto message);
    }
}