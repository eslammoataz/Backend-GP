using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceController> logger;

        public ServiceController(AppDbContext context, ILogger<ServiceController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult AddService([FromBody] ServiceDto serviceDto)
        {
            try
            {
                // Map DTO to your entity model
                var newService = new Service
                {
                    ServiceID = serviceDto.ServiceID,
                    ServiceName = serviceDto.ServiceName,
                    // Other properties...

                    // Assuming ParentServiceID is provided in the DTO for hierarchical setup
                    ParentServiceID = serviceDto.ParentServiceID,
                };

                logger.LogInformation("|||----------------------------|||");
                logger.LogInformation("new service Name : " + newService.ServiceName);
                logger.LogInformation("new service ID : " + newService.ServiceID);
                logger.LogInformation("new service ID : " + newService.ParentServiceID);


                _context.Services.Add(newService);
                _context.SaveChanges();

                // Return the newly created service
                return CreatedAtAction(nameof(GetService), new { id = newService.ServiceID }, newService);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetService(string id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceID == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
    }

    public class ServiceDto
    {

        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ParentServiceID { get; set; } // Assuming this is provided for hierarchical setup
                                                    // Other properties...
    }
}
