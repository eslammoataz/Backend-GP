using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Services.EmailService;
using WebApplication1.Services;
using WebApplication1.Models.Requests;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> workerManager;
        private readonly ILogger<CustomerController> logger;
        private readonly IEmailService emailService;
        private readonly IWorkerServices workerServices;
        private readonly RoleManager<IdentityRole> roleManager;
        public ServiceProviderController(AppDbContext _context, IConfiguration _config,
        RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
        ILogger<CustomerController> logger, IEmailService emailService, Services.IAuthenticationService authenticationService, IWorkerServices workerServices)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            workerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.workerServices = workerServices;
        }

        [HttpPost]
        [Route("SetAvailability")]
        public async Task<IActionResult> AddAvailability(AvailabilityDto availabilityDto,string workerID)
        {
            var provider = context.provider.FirstOrDefault(w => w.Id == workerID);

            if (provider == null)
            {
                return BadRequest("worker Id is not valid");
            }

            var availability = new ProviderAvailability
            {
                ServiceProviderID = workerID,
                DayOfWeek = availabilityDto.DayOfWeek,
                StartTime = availabilityDto.StartTime,
                EndTime = availabilityDto.EndTime,
                ServiceProvider= provider

            };

            context.ProviderAvailabilities.Add(availability);
            return CreatedAtAction(nameof(GetServiceProviderAvailability), new { id = availability.ServiceProvider }, availability);

        }

        [HttpGet("{id}")]
        public IActionResult GetServiceProviderAvailability(string providerId)

        {
            var provider = context.provider.FirstOrDefault(s => s.Id == providerId);
            if (provider == null)
            {
                return BadRequest("worker Id is not valid");
            }
            
            var availability = context.ProviderAvailabilities.FirstOrDefault(a => a.ServiceProviderID == provider.Id);

            if (availability == null)
            {
                return NotFound();
            }

            return Ok(availability);
        }


    }
}
