using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Requests.ServiceRequestsValidation;

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



        /// <summary>
        /// Gets all services.
        /// </summary>
        /// <remarks>Returns a list of all services.</remarks>


        [HttpPost]
        public IActionResult AddService([FromBody] AddServiceDto serviceDto)

        {
            var criteria = _context.Criterias.FirstOrDefault(s => s.CriteriaName == serviceDto.CriteriaName);

            if (criteria == null)
            {
                return BadRequest("Criteria Doesnot Exist");
            }

            try
            {
                // Map DTO to your entity model
                var newService = new Service
                {
                    ServiceName = serviceDto.ServiceName,
                    Description = serviceDto.Description,
                    Price = serviceDto.Price,
                    AvailabilityStatus = serviceDto.AvailabilityStatus,
                    OrderID = serviceDto.OrderID,
                    ParentServiceID = serviceDto.ParentServiceID,
                    Criteria = criteria,
                    CriteriaID = criteria.CriteriaID,
                };

                _context.Services.Add(newService);


                if (newService != null)
                {
                    var criteriaController = new CriteriaController(_context, logger);

                    criteriaController.AddServicesToCriteria(criteria.CriteriaID, newService.ServiceID);
                }


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



        [HttpGet("getchilds/{parentId}")]
        public IActionResult GetChildServices(string parentId)
        {
            var parentService = _context.Services
                .Include(s => s.ChildServices) // Ensure ChildServices are loaded
                .FirstOrDefault(s => s.ServiceID == parentId);

            if (parentService == null)
            {
                return NotFound("Parent service not found.");
            }

            var childServices = parentService.ChildServices ?? new List<Service>();

            if (childServices.Count == 0)
            {
                return Ok("This Service has no Subservices");
            }
            else
            {
                return Ok(childServices);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetService(string id)
        {
            var service = _context.Services.Include(s => s.ChildServices).FirstOrDefault(s => s.ServiceID == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
    }



}
