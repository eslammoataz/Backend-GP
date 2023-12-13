using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Security.Claims;
using WebApplication1.Helpers;
using WebApplication1.Models.Requests.AuthRequests;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Controllers.UsersControllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> usermanager;
        private readonly IConfiguration config;


        public LoginController(UserManager<User> userManager, IConfiguration config)
        {
            usermanager = userManager;
            this.config = config;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> login(LoginRequestDto loginRequest)
        {
            var user = await usermanager.FindByEmailAsync(loginRequest.Email);
            var isValidPassword = await usermanager.CheckPasswordAsync(user, loginRequest.password);

            if (user == null || !isValidPassword)
            {
                return NotFound(new Response { Status = "Error", Message = "Invalid credentials" });
            }

            List<Claim> claims = new()
                    {
                          new Claim("CustomerId", user.Id)

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
