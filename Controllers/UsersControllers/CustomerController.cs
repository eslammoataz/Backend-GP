using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
using WebApplication1.Models.Requests.ServiceRequestsValidation;
using WebApplication1.Services;
using WebApplication1.Services.Abstractions;
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
        private readonly ICustomerService customerService;


        public CustomerController(AppDbContext _context, IConfiguration _config,
                RoleManager<IdentityRole> _roleManager, UserManager<User> _customerManager,
                ILogger<CustomerController> logger, IEmailService emailService, IEmailConfirmService emailConfirm, IAuthenticationService authenticationService, ICustomerService customerService)
        {
            context = _context;
            config = _config;
            roleManager = _roleManager;
            customerManager = _customerManager;
            this.logger = logger;
            this.emailService = emailService;
            this.emailConfirm = emailConfirm;
            this.authenticationService = authenticationService;
            this.customerService = customerService;
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

            if (Response.isError)
                return BadRequest(Response);

            return Ok(Response);

        }

        [HttpPost]
        [Route("addtocart")]
        public async Task<IActionResult> AddToCart(RequestServiceDto requestServiceDto, string customerId)
        {
            var Response = await customerService.RequestService(requestServiceDto, customerId);

            if (Response.isError)
            {
                return BadRequest(Response);
            }
            return Ok(Response);

        }

        [HttpPost]
        [Route("OrderCart/{customerId}")]
        public async Task<IActionResult> OrderCart(string customerId)
        {
            var Response = await customerService.OrderService(customerId);

            if (Response.isError)
            {
                return BadRequest(Response);
            }
            return Ok(Response);

        }

        [HttpPost]
        [Route("removefromCart/{customerId}/{itemId}")]
        public async Task<IActionResult> CancelRequestService(string customerId, string itemId)
        {
            var Response = await customerService.CancelRequestService(customerId, itemId);

            if (Response.isError)
            {
                return BadRequest(Response);
            }
            return Ok(Response);

        }

        [HttpGet("getcustomercart/{customerId}")]
        public async Task<IActionResult> GetCustomerCart(string customerId)

        {
            var Response = await customerService.GetCustomerCart(customerId);

            if (Response.Payload == null)
            {
                return BadRequest(Response.Message);
            }

            return Ok(Response);
        }






    }
}
