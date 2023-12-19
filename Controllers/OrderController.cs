using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Requests.OrderRequestsValidations;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceController> logger;

        public OrderController(AppDbContext context, ILogger<ServiceController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> RequestOrder(RequestOrderDto orderDto, string customerID)
        {

            try
            {
                var workerservice = _context.WorkerServices.FirstOrDefault(w => w.WorkerID == orderDto.WorkerID && w.ServiceID==orderDto.ServiceID);
                if (workerservice == null)
                {
                    return NotFound();
                }
                var order = new Order()
                {
                   
                    CustomerID = customerID,
                    WorkerServices= new List<WorkerService>() { workerservice },
                    OrderStatusID = "1",
                };

                _context.Orders.Add(order);

                _context.SaveChanges();


                // Return the newly created service
                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(string id)
        {
            var order = _context.Orders.FirstOrDefault(o=>o.OrderID == id);

            if ( order== null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}



   

