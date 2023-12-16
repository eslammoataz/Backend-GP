using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using System.Security.Claims;

using WebApplication1.Helpers;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Services.EmailService;
using WebApplication1.Models.Emails;
using System.Security.Policy;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using MailKit.Net.Smtp;
using WebApplication1.Services;
using Microsoft.AspNetCore.WebUtilities;
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Services.EmailService;
using Microsoft.AspNetCore.Routing;
using WebApplication1.Models.Requests.AuthRequests;
using NuGet.Protocol.Plugins;

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


        public async Task<Response> Register(User user, string role, string password)
        {
            var userExist = await userManager.FindByEmailAsync(user.Email);
            var roleExist = await roleManager.RoleExistsAsync(role);


            if (userExist != null)
            {
                return new Response { Status = "Error", Message = "This email is already registered" };

            }

            if (!roleExist)
            {
                return new Response { Status = "Error", Message = "This Role doesnot Exist" };
            }


            // it stores the password hashed already without using bcrypt
            IdentityResult result = await userManager.CreateAsync(user, password);

            // User Registered 
            if (result == null || !result.Succeeded)
            {

                var errorDescriptions = result.Errors.Select(error => error.Description).ToList();

                return new Response
                {
                    Status = "Error",
                    Message = "User Registration Failed",
                    Errors = errorDescriptions
                };
            }


            List<Claim> claims = new()
            {
                  new Claim("userId", user.Id)

            };

            var tokenString = JWT.generateToken(claims, config);

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

            return new Response { Status = "Success", Message = $"User Registered Successfully , Verification Email sent to {user.Email} " };

        }

        public async Task<Response> Login(LoginRequestDto loginRequestDto)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.password);

            if (user == null || !isValidPassword)
            {
                return new Response { Status = "Error", Message = "Invalid credentials" };
            }

            List<Claim> claims = new()
            {
                  new Claim("userId", user.Id)

            };

            var tokenString = JWT.generateToken(claims, config);


            // Set the token in a cookie

            httpContextAccessor.HttpContext.Response.Cookies.Append("Auth", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
            });


            return new Response { Status = "Success", Message = "User logged in Successfully" };
        }
    }
}
