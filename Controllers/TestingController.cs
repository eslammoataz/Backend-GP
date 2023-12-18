using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult AddService([FromBody] ServiceDto serviceDTO)
        {
            // Map DTO to your entity model
            var newService = new Service
            {
                ServiceName = serviceDTO.ServiceName,
                Description = serviceDTO.Description,
                Price = serviceDTO.Price,
                AvailabilityStatus = serviceDTO.AvailabilityStatus,
                OrderID = serviceDTO.OrderID,
                ParentServiceID = serviceDTO.ParentServiceID
            };


            _context.Services.Add(newService);
            _context.SaveChanges();

            return Ok("Service added successfully!");
        }



        [HttpGet]
        public ActionResult<object> GetServiceWithChildren()
        {
            var parentId = "15fb7477-688c-4b1a-8380-c2fd6430e66d"; // Assuming this is the parent service ID

            var parentService = _context.Services.Include(s => s.ChildServices)
                                                 .FirstOrDefault(s => s.ServiceID == parentId);

            if (parentService == null)
                return BadRequest("Parent service not found");

            var serviceDTO = MapToDTO(parentService);

            return Ok(serviceDTO);
        }

        private ServiceDTO MapToDTO(Service service)
        {
            var serviceDTO = new ServiceDTO
            {
                ServiceID = service.ServiceID,
                ServiceName = service.ServiceName,
                Description = service.Description,
                Price = service.Price ?? 0,
                ChildServices = GetChildServices(service.ChildServices)
            };

            return serviceDTO;
        }

        private List<ServiceDTO> GetChildServices(List<Service> services)
        {
            var childDTOs = new List<ServiceDTO>();

            if (services != null && services.Any())
            {
                foreach (var child in services)
                {
                    var childDTO = MapToDTO(child);
                    childDTOs.Add(childDTO);

                    var grandChildDTOs = GetChildServices(child.ChildServices);
                    childDTOs.AddRange(grandChildDTOs);
                }
            }

            return childDTOs;
        }

    }


    public class ServiceDTO
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ServiceDTO> ChildServices { get; set; } = new List<ServiceDTO>(); // Updated property
    }



    public class ServiceDto
    {
        public string ServiceName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? AvailabilityStatus { get; set; }
        public string? OrderID { get; set; }
        public string? ParentServiceID { get; set; }

    }

}
