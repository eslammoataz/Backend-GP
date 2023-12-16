using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
using WebApplication1.Models.Requests.AuthRequests;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly ILogger<AuthenticationController> logger;
        private readonly IAuthenticationService authenticationService;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthenticationController(AppDbContext _context, IConfiguration _config,
            RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager, IEmailService _emailService,
            ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            userManager = _userManager;
            emailService = _emailService;
            this.logger = logger;
            this.authenticationService = authenticationService;
        }




        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginRequestDto loginRequest)
        {
            var Response = await authenticationService.Login(loginRequest);

            if (Response.Status == "Error")
            {
                return Unauthorized(Response);
            }
            return Ok(Response);

        }
    }


}
