using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Emails;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
using WebApplication1.Services;

namespace WebApplication1.Controllers.UsersControllers
{

    // [Route("api/[controller]")]
    //[ApiController]
    public class WorkerController : ControllerBase
    {

        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> workerManager;
        private readonly ILogger<CustomerController> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;


        public WorkerController(AppDbContext _context, IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<CustomerController> logger, IEmailService emailService)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            workerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> register(RegisterWorker registrationDto)
        {
            var userExist = await workerManager.FindByEmailAsync(registrationDto.Email);

            if (userExist != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "This email is already registered" });

            }


            var user = new Worker()
            {
                Email = registrationDto.Email,
                UserName = registrationDto.UserName,
                PhoneNumber = registrationDto.PhoneNumber,
                LastName = registrationDto.LastName,
                FirstName = registrationDto.FirstName,
                CriminalRecord = registrationDto.CriminalRecord,
                NationalID = registrationDto.NationalID,
                //services 
                //photos
            };

            // it stores the password hashed already without using bcrypt
            var result = await workerManager.CreateAsync(user, registrationDto.Password);

            // User Registered 
            if (!result.Succeeded)
                return BadRequest(new { Result_Error = result.Errors, Response = new Response { Status = "Error", Message = "User Registeration Failed" } });

            List<Claim> claims = new()
            {
                  new Claim("WorkerID", user.Id)

            };

            var tokenString = JWT.generateToken(claims, config);

            Response.Cookies.Append("Auth", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
            });

            ////Add Token to Verify the email....
            //var token = await workerManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Customer", new { token, email = user.Email }, Request.Scheme);
            //var message = new EmailDto(user.Email!, "Sarvicny: Your application is filled successfully ", "Sarvicny Account Confirmation Link : " + confirmationLink! + "You will be notified later with your interview time");

            //emailService.SendEmail(message);


            return Ok(new Response { Status = "Success", Message = $"User Registered Successfully , Confirmation Email sent to {user.Email}" });
        }

        //public async Task<IActionResult> ConfirmEmail(string token, string email)
        //{
        //    var user = await workerManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var result = await workerManager.ConfirmEmailAsync(user, token);
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
