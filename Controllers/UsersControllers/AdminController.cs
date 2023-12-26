using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Emails;
using WebApplication1.Services.EmailService;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly ILogger<AdminController> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminController(AppDbContext _context, UserManager<User> _adminManager, RoleManager<IdentityRole> _roleManager, IConfiguration _config, ILogger<AdminController> _logger, IEmailService _emailService)
        {
            context = _context;
            userManager = _adminManager;
            roleManager = _roleManager;
            config = _config;
            logger = _logger;
            emailService = _emailService;

        }


        [HttpGet("getCustomers")]
        public IActionResult GetAllCustomers()
        {
            var customers = context.Customers.Select(c => new
            {
                c.Id,
                c.FirstName,
                c.LastName,
                c.Address,
                c.Email,
                Cart = c.Cart.ServiceRequests.Select(s => new
                {
                    s.AddedTime,
                    Service = s.providerService.Service.ServiceName,
                    Provider = s.providerService.Provider.FirstName,


                }).ToList()
            }).ToList();
            if (customers.Any())
            {
                return Ok(customers);
            }
            else
            {
                return BadRequest("No Customers to retrive");

            }
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

            var provider = context.Provider.FirstOrDefault(w => w.Id == WorkerID);
            if (provider == null)
            {
                return BadRequest("Wrong Worker ID");
            }

            provider.isVerified = true;
            context.SaveChanges();

            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(provider);

            logger.LogInformation(provider.Email);

            var message = new EmailDto(provider.Email!, "Sarvicny: Worker Approved Successfully", "Congratulations you are accepted");

            emailService.SendEmail(message);
            return Ok($"Worker Approved Successfully , Verification Email sent to {provider.Email} ");

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

