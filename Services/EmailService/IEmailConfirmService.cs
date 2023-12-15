using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Services.EmailService
{
    public interface IEmailConfirmService
    {
        Task<bool> ConfirmEmailAsync(string token, string email);

        public string GenerateConfirmationLink(string token, string email, HttpContext httpContext = null);


    }
}
