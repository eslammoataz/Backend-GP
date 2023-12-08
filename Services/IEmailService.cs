using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto message);
    }
}