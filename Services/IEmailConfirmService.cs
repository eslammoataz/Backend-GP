using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Services
{
    public interface IEmailConfirmService
    {
        Task<bool> ConfirmEmailAsync(string token, string email);

        public string GenerateConfirmationLink(User user);


    }
}
