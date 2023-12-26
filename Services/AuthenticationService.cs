using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Emails;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.AuthRequests;
using WebApplication1.Services.EmailService;

namespace WebApplication1.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly ILogger<AuthenticationService> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailConfirmService emailConfirm;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;
        private readonly LinkGenerator linkGenerator;

        public AuthenticationService(IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<AuthenticationService> logger, IEmailService emailService, IEmailConfirmService emailConfirm,
                IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            config = _config;
            roleManager = _roleManager;
            userManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.emailConfirm = emailConfirm;
            this.httpContextAccessor = httpContextAccessor;
            //this.urlHelper = urlHelper;
            this.linkGenerator = linkGenerator;
        }


  public async Task<Response<string>> Register(User user, string role, string password)
        {
            var userExist = await userManager.FindByEmailAsync(user.Email);
            var roleExist = await roleManager.RoleExistsAsync(role);

            var response = new Response<string>();
            if (userExist != null)
            {
                response.isError= true;
                response.Errors.Add("This email is already registered");
                return response;

            }

            if (!roleExist)
            {
                response.isError= true;
                response.Errors.Add("This Role doesnot Exist");
                return response;
            }


            // it stores the password hashed already without using bcrypt
            IdentityResult result = await userManager.CreateAsync(user, password);

            // User Registered 
            if (result == null || !result.Succeeded)
            {
                response.isError= true;
                response.Errors = result.Errors.Select(error => error.Description).ToList();
                return response;
            }


            List<Claim> claims = new()
            {
                  new Claim("userId", user.Id)

            };

            var tokenString = JWT.generateToken(claims, config);

            // Add the token to the headers
            httpContextAccessor.HttpContext.Request.Headers.Add("x-Authorization", $"Bearer {tokenString}");

            httpContextAccessor.HttpContext.Response.Cookies.Append("Auth", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
            });



            await userManager.AddToRoleAsync(user, role);

            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);


            var confirmationLink = linkGenerator.GetUriByAction(httpContextAccessor.HttpContext, "ConfirmEmail", "Authentication", new { token, email = user.Email });
            var message = new EmailDto(user.Email!, "Sarvicny: Confirmation email link", "Sarvicny Account Confirmation Link : " + confirmationLink!);

            emailService.SendEmail(message);
            
            response.Message =  $"User Registered Successfully , Verification Email sent to {user.Email} ";
            response.Payload = tokenString;

            return response;

        }


        public async Task<Response<object>> Login(LoginRequestDto loginRequestDto)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.password);
            
            var response = new Response<object>();

            if (user == null || !isValidPassword)
            {
                response.isError= true;
                response.Errors.Add("Invalid Credentials");
                return response;
            }
            var roles = await userManager.GetRolesAsync(user);
            List<Claim> claims = new()
            {
                  new Claim("userId", user.Id)

            };

            var tokenString = JWT.generateToken(claims, config);

            httpContextAccessor.HttpContext.Response.Headers.Add("x-Authorization", $" Bearer {tokenString}");
            
            // Set the token in a cookie
            httpContextAccessor.HttpContext.Response.Cookies.Append("Auth", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
            });

            var payload = new
            {
                tokenString = tokenString,
                Id=user.Id,
                role = roles.FirstOrDefault(),

        };
            
            response.Message = "User logged in Successfully";
            response.Payload = payload;
            return response;
        }
    }
}
