using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Requests.ServiceRequestsValidation;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceController> logger;

        public CriteriaController(AppDbContext context, ILogger<ServiceController> logger)
        {
            _context = context;
            this.logger = logger;
        }
        [HttpPost]
        
        public IActionResult AddCriteria([FromBody] AddCriteriaValidation criteriaDto)
        {
            try
            {
                // Map DTO to your entity model
                var newCriteria = new Criteria
                {
                    CriteriaID = criteriaDto.CriteriaID,
                    CriteriaName = criteriaDto.CriteriaName,
                    Description = criteriaDto.Description,

                };

                _context.Criterias.Add(newCriteria);
                _context.SaveChanges();

                // Return the newly created service
                return CreatedAtAction(nameof(GetCriteria), new { id = newCriteria.CriteriaID }, newCriteria);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCriteria(string id)
        {
            var criteria = _context.Criterias.FirstOrDefault(s => s.CriteriaID == id);

            if (criteria == null)
            {
                return NotFound();
            }

            return Ok(criteria);
        }


        [HttpPost]
        [Route("addservice")]
        public IActionResult AddServicesToCriteria(string criteriaId, [FromBody] string serviceId)
        {
            var criteria = _context.Criterias.Include(c => c.Services).FirstOrDefault(c => c.CriteriaID ==criteriaId);

            if (criteria == null)
            {
                return NotFound($"Criteria with ID {criteriaId} not found.");
            }


            var existingService = _context.Services.FirstOrDefault(s => s.ServiceID == serviceId);

            if (existingService != null)
            {
                criteria.Services.Add(existingService);
            }
            else
            {
                // Handle the case where the service with the specified ID is not found.
                // You may want to return an error response or take appropriate action.
                return NotFound($"Service with ID {existingService.ServiceID} not found.");
            }
            
            _context.SaveChanges();

            return Ok("Services added successfully to the criteria.");
        }
    }
}


