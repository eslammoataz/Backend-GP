﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AddCriteria([FromBody] AddCriteriaDto criteriaDto)
        {

            // Map DTO to your entity model
            var newCriteria = new Criteria
            {
                CriteriaName = criteriaDto.CriteriaName,
                Description = criteriaDto.Description,

            };

            _context.Criterias.Add(newCriteria);
            _context.SaveChanges();

            // Return the newly created service
            return CreatedAtAction(nameof(GetCriteria), new { id = newCriteria.CriteriaID }, newCriteria);

        }


        [HttpGet("{id}")]
        public IActionResult GetCriteria(string id)
        {
            var criteria = _context.Criterias.Include(c => c.Services).FirstOrDefault(s => s.CriteriaID == id);

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
            var criteria = _context.Criterias.Include(c => c.Services).FirstOrDefault(c => c.CriteriaID == criteriaId);

            if (criteria == null)
            {
                return NotFound($"Criteria with ID {criteriaId} not found.");
            }

            var existingService = _context.Services.FirstOrDefault(s => s.ServiceID == serviceId);

            if (existingService != null)
            {
                // Check if the service already exists in the criteria
                var serviceExists = criteria.Services?.Any(s => s.ServiceID == serviceId);

                if (serviceExists == true)
                {
                    return BadRequest($"Service with ID {serviceId} already exists in the criteria.");
                }

                criteria.Services ??= new List<Service>();
                criteria.Services.Add(existingService);
                _context.SaveChanges();

                return Ok($"Service with ID {serviceId} added successfully to the criteria.");
            }

            return NotFound($"Service with ID {serviceId} not found.");
        }
    }
}


