using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Models
{
    public class DataSeeder
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;

        public DataSeeder(AppDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public void seed()
        {
            if (!context.Admins.Any())
            {
                var admin = new Admin
                {
                    Id = "1",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(admin, "admin");
            }
        }
    }
}
