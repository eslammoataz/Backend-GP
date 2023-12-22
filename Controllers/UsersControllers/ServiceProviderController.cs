using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Services.EmailService;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Requests.AvailabilityRequestsValidations;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Models.Entities.Users.ServiceProviders;

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
        public async Task<IActionResult> AddAvailability(AvailabilityDto availabilityDto, string workerID)
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
                AvailabilityDate=DateTime.Now,
                ServiceProvider = provider,
    
            };

            context.ProviderAvailabilities.Add(availability);
            return CreatedAtAction(nameof(GetServiceProviderAvailability), new { id = availability.ServiceProvider }, availability);

        }
        [HttpPost]
        [Route("SetAvailabilitySlots")]
        public async Task<IActionResult> AddAvailabilitySlots(TimeSlotDto slotDto, string availabilityId)
        {
            var availability = context.ProviderAvailabilities.FirstOrDefault(a => a.ProviderAvailabilityID == availabilityId);

            if (availability == null)
            {
                return BadRequest("worker Id is not valid");
            }

            var slot = new TimeSlot
            {
                ProviderAvailabilityID = availability.ProviderAvailabilityID,
                StartTime = slotDto.StartTime,
                EndTime = slotDto.EndTime,
                enable=true,

            };

            context.Slots.Add(slot);
            return CreatedAtAction(nameof(GetServiceProviderSlots), new { id = availability.ProviderAvailabilityID }, availability);

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

            return CreatedAtAction(nameof(GetServiceProviderSlots), new { id = availability.ProviderAvailabilityID }, availability);
        }


        [HttpGet("{availability/{id}")]
        public IActionResult GetServiceProviderSlots(string avalId)

        {
            var availability = context.ProviderAvailabilities.FirstOrDefault(a => a.ProviderAvailabilityID == avalId);
            if (availability == null)
            {
                return BadRequest("Availability Id is not valid");
            }

            var slots = context.Slots.Where(s=> s.ProviderAvailabilityID == availability.ProviderAvailabilityID).ToList();

            if (slots == null)
            {
                return NotFound();
            }

            return Ok(slots);
        }
        [HttpPost]
        [Route("SetAvailabilityAsPrev")]
        public IActionResult SetServiceProviderAvailabilityAsPrev(string providerId)

        {
            var provider = context.provider.FirstOrDefault(s => s.Id == providerId);
            if (provider == null)
            {
                return BadRequest("worker Id is not valid");
            }
            DateTime currentDate = DateTime.Now;
            DateTime previousWeekStart = currentDate.AddDays(-7);
            DateTime previousWeekEnd = currentDate.AddDays(-1);


            var previousWeekAvailability = context.ProviderAvailabilities
                .FirstOrDefault(pa => pa.AvailabilityDate >= previousWeekStart && pa.AvailabilityDate <= previousWeekEnd);

            if (previousWeekAvailability == null)
            {
                return BadRequest("ther is no data in the previous week");
            }

            var newavailability = new ProviderAvailability
            {
                ServiceProviderID = provider.Id,
                DayOfWeek = previousWeekAvailability.DayOfWeek,
                AvailabilityDate = currentDate,
                ServiceProvider = provider,

            };

            context.ProviderAvailabilities.Add(newavailability);
            return CreatedAtAction(nameof(GetServiceProviderAvailability), new { id = newavailability.ServiceProvider }, newavailability);
        }

    }
}
