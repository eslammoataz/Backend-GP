using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Emails;
using WebApplication1.Services.EmailService;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Services;

namespace WebApplication1.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly ILogger<AdminController> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminController(IAdminService adminService ,AppDbContext _context, UserManager<User> _adminManager, RoleManager<IdentityRole> _roleManager, IConfiguration _config, ILogger<AdminController> _logger, IEmailService _emailService)
        {
            this.adminService = adminService;
            context = _context;
            userManager = _adminManager;
            roleManager = _roleManager;
            config = _config;
            logger = _logger;
            emailService = _emailService;

        }


        [HttpGet("getCustomers")]
        public async Task <IActionResult> GetAllCustomers()
        {
            return Ok(await adminService.GetAllCustomers());
        }
        
        
        [HttpGet("getServiceProviders")]
        public async Task<IActionResult> GetAllServiceProviders()
        {
            return Ok(await adminService.GetAllServiceProviders());
        }
        
        
        [HttpGet("getServices")]
        public async Task<IActionResult> GetAllServices()
        {
            return Ok(await adminService.GetAllServices());
        }

            

        [HttpGet("getRequests")]
        public IActionResult GetRequests()
        {
            var unHandeledProviders = context.Provider.Where(w => w.isVerified==false).ToList();

            if (unHandeledProviders.Count == 0)
            {
                return NotFound("No Requests found");
            }
            else
            {
                return Ok(unHandeledProviders);
            }


        }


        [HttpPost]
        [Route("ApproveServiceProvider")]
        public async Task<IActionResult> ApproveServiceProviderRegister(string WorkerID)
        {

            var response = await adminService.ApproveServiceProviderRegister(WorkerID);
            if(response.isError)
            {
                return NotFound("Not Found");
            }
            
            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(response.Payload);

            logger.LogInformation(response.Payload.Email);

            var message = new EmailDto(response.Payload.Email!, "Sarvicny: Worker Approved Successfully", "Congratulations you are accepted");

            emailService.SendEmail(message);
            return Ok($"Worker Approved Successfully , Verification Email sent to {response.Payload.Email} ");

        }


        [HttpPost]
        [Route("RejectServiceProvider")]
        public async Task<IActionResult> RejectServiceProviderRegister(string WorkerID)
        {

            var provider = context.Provider.FirstOrDefault(w => w.Id == WorkerID&& w.isVerified==false);
            if (provider == null)
            {
                return BadRequest("Wrong Worker ID");
            }

           
            context.Provider.Remove(provider);
            context.SaveChanges();

            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(provider);


            var message = new EmailDto(provider.Email!, "Sarvicny: Worker Rejected", "Sorry you are rejected" );

            emailService.SendEmail(message);
            
            return Ok($" Worker Rejected Successfully, Verification Email sent to {provider.Email} ");

        }





    }
}

