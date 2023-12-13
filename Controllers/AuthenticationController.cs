﻿//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Net.Mail;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using MimeKit;
//using MimeKit.Text;
//using WebApplication1.Data;
//using WebApplication1.Helpers;
//using WebApplication1.Models;
//using MailKit.Net.Smtp;
//using WebApplication1.Services;
//using Microsoft.AspNetCore.WebUtilities;

//namespace WebApplication1.Controllers
//{
//    [ApiController]
//    [Route("api/auth")]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly ApplicationDbContext context;
//        private readonly IConfiguration config;
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly IEmailService emailService;
//        private readonly ILogger<AuthenticationController> logger;
//        private readonly RoleManager<IdentityRole> roleManager;

//        public AuthenticationController(ApplicationDbContext _context, IConfiguration _config,
//            RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager, IEmailService _emailService,
//            ILogger<AuthenticationController> logger)
//        {
//            context = _context;
//            config = _config;
//            roleManager = _roleManager;
//            userManager = _userManager;
//            emailService = _emailService;
//            this.logger = logger;
//        }


//        [HttpPost]
//        [Route("register")]

//        public async Task<IActionResult> register(RegisterUser registrationDto, [FromQuery] string role)
//        {
//            var userExist = await userManager.FindByEmailAsync(registrationDto.Email);
//            var roleExist = await roleManager.RoleExistsAsync(role);


//            if (userExist != null)
//            {
//                return BadRequest(new Response { Status = "Error", Message = "This email is already registered" });

//            }

//            if (!roleExist)
//            {
//                return BadRequest(new Response { Status = "Error", Message = "This Role doesnot Exist" });
//            }

//            IdentityUser user = new IdentityUser()
//            {
//                Email = registrationDto.Email,
//                UserName = registrationDto.UserName
//            };

//            // it stores the password hashed already without using bcrypt
//            var result = await userManager.CreateAsync(user, registrationDto.Password);

//            // User Registered 
//            if (!result.Succeeded)
//                return BadRequest(new { Result_Error = result.Errors, Response = new Response { Status = "Error", Message = "User Registeration Failed" } });

//            List<Claim> claims = new()
//            {
//                  new Claim("userId", user.Id)

//            };

//            var tokenString = JWT.generateToken(claims, config);

//            Response.Cookies.Append("Auth", tokenString, new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
//                SameSite = SameSiteMode.Strict,
//                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
//            });

//            await userManager.AddToRoleAsync(user, role);

//            //Add Token to Verify the email....
//            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
//            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
//            var message = new EmailDto(user.Email!, "Sarvicny: Confirmation email link", "Sarvicny Account Confirmation Link : " + confirmationLink!);

//            emailService.SendEmail(message);


//            return Ok(new Response { Status = "Success", Message = $"User Registered Successfully , Verification Email sent to {user.Email}" });
//        }

//        [HttpGet("ConfirmEmail")]
//        public async Task<IActionResult> ConfirmEmail(string token, string email)
//        {
//            var user = await userManager.FindByEmailAsync(email);
//            if (user != null)
//            {
//                var result = await userManager.ConfirmEmailAsync(user, token);
//                if (result.Succeeded)
//                {
//                    return StatusCode(StatusCodes.Status200OK,
//                      new Response { Status = "Success", Message = "Email Verified Successfully" });
//                }
//            }
//            return StatusCode(StatusCodes.Status500InternalServerError,
//                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
//        }


//        [HttpPost]
//        [Route("login")]
//        public async Task<IActionResult> login(LoginRequestDto loginRequest)
//        {
//            var user = await userManager.FindByEmailAsync(loginRequest.email);
//            var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequest.password);

//            if (user == null || !isValidPassword)
//            {
//                return NotFound(new Response { Status = "Error", Message = "Invalid credentials" });
//            }

//            List<Claim> claims = new()
//            {
//                  new Claim("userId", user.Id)

//            };

//            var tokenString = JWT.generateToken(claims, config);

//            // Set the token in the response header
//            Response.Headers.Add("Authorization", "Bearer " + tokenString);

//            // Set the token in a cookie
//            Response.Cookies.Append("Auth", tokenString, new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
//                SameSite = SameSiteMode.Strict,
//                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
//            });

//            return Ok(new Response { Status = "Success", Message = "User logged in Successfully" });

//        }


//        //[HttpPost]

//        //public IActionResult sendEmail(EmailDto message)
//        //{

//        //    emailService.SendEmail(message);

//        //    return Ok();

//        //}


//    }
//}