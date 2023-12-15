using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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
using WebApplication1.Services.EmailService;

namespace WebApplication1.Controllers.UsersControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {

        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> workerManager;
        private readonly ILogger<CustomerController> logger;
        private readonly IEmailService emailService;
        private readonly Services.IAuthenticationService authenticationService;
        private readonly RoleManager<IdentityRole> roleManager;


        public WorkerController(AppDbContext _context, IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<CustomerController> logger, IEmailService emailService, Services.IAuthenticationService authenticationService)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            workerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> register(RegisterWorker registrationDto, string role)
        {
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

            logger.LogInformation("|||---------------|||");
            logger.LogInformation("INSIDE REGISTER WORKER");

            var Response = await authenticationService.Register(user, role, registrationDto.Password);

            if (Response.Errors != null && Response.Errors.Count > 0)
                return BadRequest(Response);

            return Ok(Response);

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
