using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
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
        private readonly Services.IServiceProviderService workerServices;
        private readonly RoleManager<IdentityRole> roleManager;


        public WorkerController(AppDbContext _context, IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<CustomerController> logger, IEmailService emailService, Services.IAuthenticationService authenticationService, Services.IServiceProviderService workerServices)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            workerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.authenticationService = authenticationService;
            this.workerServices = workerServices;
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
                isVerified = false
                //services 
                //photos
            };

            var Response = await authenticationService.Register(user, role, registrationDto.Password);

            if (Response.Status == "Error")
                return BadRequest(Response);

            return Ok(Response);

        }

        [HttpPost]
        [Route("registerService")]

        public async Task<IActionResult> registerService(string workerId, string serviceId)
        {
            var response = await workerServices.RegisterService(workerId, serviceId);

            if (response.Status == "Error")
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("getServices")]
        public async Task<IActionResult> getRegisteredServices(string workerId)
        {
            return Ok(await workerServices.GetRegisteredServices(workerId));
        }

    }
}
