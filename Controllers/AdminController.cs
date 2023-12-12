using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> adminManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly ILogger<AdminController> logger;

        public AdminController(AppDbContext _context, UserManager<User> _adminManager, RoleManager<IdentityRole> _roleManager, IConfiguration _config, ILogger<AdminController> _logger)
        {
            context = _context;
            adminManager = _adminManager;
            roleManager = _roleManager;
            config = _config;
            logger = _logger;
        }


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> login(LoginRequestDto loginRequest)
        {

            var user = await adminManager.FindByEmailAsync(loginRequest.Email);

            logger.LogInformation("--------------------------------------------");
            logger.LogInformation(user.Email);
            logger.LogInformation("--------------------------------------------");

            var isValidPassword = (user.PasswordHash == loginRequest.password);

            if (user == null || !isValidPassword)
            {
                return NotFound(new Response { Status = "Error", Message = "Invalid credentials" });
            }

            List<Claim> claims = new()
                    {
                          new Claim("AdminId", user.Id)

                    };

            var tokenString = JWT.generateToken(claims, config);

            // Set the token in the response header
            Response.Headers.Add("Authorization", "Bearer " + tokenString);

            // Set the token in a cookie
            Response.Cookies.Append("Auth", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
            });

            return Ok(new Response { Status = "Success", Message = "User logged in Successfully" });


        }
    }
}
