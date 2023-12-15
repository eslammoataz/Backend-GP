using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Emails;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.AuthRequests;
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
using WebApplication1.Services;
using WebApplication1.Services.EmailService;

namespace WebApplication1.Controllers.UsersControllers
{

    //[Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> customerManager;
        private readonly ILogger<CustomerController> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailConfirmService emailConfirm;
        private readonly IAuthenticationService authenticationService;

        public CustomerController(AppDbContext _context, IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<CustomerController> logger, IEmailService emailService, IEmailConfirmService emailConfirm, IAuthenticationService authenticationService)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            customerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.emailConfirm = emailConfirm;
            this.authenticationService = authenticationService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterCustomer registrationDto, string role)
        {
            var user = new Customer()
            {
                Email = registrationDto.Email,
                UserName = registrationDto.UserName,
                PhoneNumber = registrationDto.PhoneNumber,
                Address = registrationDto.Address,
                LastName = registrationDto.LastName,
                FirstName = registrationDto.FirstName,
            };

            var Response = await authenticationService.Register(user, role, registrationDto.Password);

            if (Response.Errors != null && Response.Errors.Count > 0)
                return BadRequest(Response);

            return Ok(Response);

        }


        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> login(LoginRequestDto loginRequest)
        //{
        //    var user = await customerManager.FindByEmailAsync(loginRequest.Email);
        //    var isValidPassword = await customerManager.CheckPasswordAsync(user, loginRequest.password);

        //    if (user == null || !isValidPassword)
        //    {
        //        return NotFound(new Response { Status = "Error", Message = "Invalid credentials" });
        //    }

        //    List<Claim> claims = new()
        //            {
        //                  new Claim("CustomerId", user.Id)

        //            };

        //    var tokenString = JWT.generateToken(claims, config);

        //    // Set the token in the response header
        //    Response.Headers.Add("Authorization", "Bearer " + tokenString);

        //    // Set the token in a cookie
        //    Response.Cookies.Append("Auth", tokenString, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
        //        SameSite = SameSiteMode.Strict,
        //        Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
        //    });

        //    return Ok(new Response { Status = "Success", Message = "User logged in Successfully" });

        //}


        //[HttpGet("ConfirmEmail")]
        //public async Task<IActionResult> ConfirmEmail(string token, string email)
        //{
        //    var user = await customerManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var result = await customerManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return StatusCode(StatusCodes.Status200OK,
        //              new Response { Status = "Success", Message = "Email Verified Successfully" });
        //        }
        //    }
        //    return StatusCode(StatusCodes.Status500InternalServerError,
        //               new Response { Status = "Error", Message = "This User Doesnot exist!" });
        //}
    }
}
