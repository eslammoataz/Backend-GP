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
                    Description = serviceDto.Description,
                    Price = serviceDto.Price,
                    AvailabilityStatus = serviceDto.AvailabilityStatus,
                    OrderID = serviceDto.OrderID,
                    ParentServiceID = serviceDto.ParentServiceID
                    // Map other properties...
                };

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
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? AvailabilityStatus { get; set; }
        public string? OrderID { get; set; }
        public string? ParentServiceID { get; set; }
        // Other properties...

        // You might also include properties for related entities if needed
    }

}
