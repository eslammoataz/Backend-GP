using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult AddService([FromBody] AddServiceValidation serviceDto)

        {
            var criteria = _context.Criterias.FirstOrDefault(s => s.CriteriaName == serviceDto.Criteria);

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
                    //OrderID = serviceDto.OrderID,
                    //ParentServiceID = serviceDto.ParentServiceID,
                    Criteria = criteria,
                    CriteriaID = criteria.CriteriaID,
                    
                    
                };
                
                if (newService != null) {
                    var criteriaController = new CriteriaController(_context,logger);

                    criteriaController.AddServicesToCriteria(criteria.CriteriaID, newService.ServiceID);
                }

                
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



}
