using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.UsersControllers;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Services
{
    public class EmailConfirmService:IEmailConfirmService
    {
        private readonly UserManager<User> userManager;
        private readonly IUrlHelper urlHelper;
        private readonly IServiceProvider serviceProvider;

        public EmailConfirmService(UserManager<User> userManager, IUrlHelper urlHelper, IServiceProvider serviceProvider)
        {
            this.userManager = userManager;
            this.urlHelper = urlHelper;
            this.serviceProvider = serviceProvider;

        }


        public async Task<bool> ConfirmEmailAsync(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                return result.Succeeded;
            }
            return false;
        }
        public string GenerateConfirmationLink(User user)
        {
            var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;

            using (var scope = serviceProvider.CreateScope())
            {
                var urlHelper = scope.ServiceProvider.GetRequiredService<IUrlHelper>();
                var confirmationLink = urlHelper.Action(nameof(ConfirmEmailController.Confirm), "EmailConfirmation", new { token, email = user.Email }, "https");
                return confirmationLink;
            }

            
        }
    }
}
