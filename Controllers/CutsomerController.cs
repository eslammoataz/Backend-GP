//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Data;

//namespace WebApplication1.Controllers
//{
//    [ApiController]
//    [Route("api/customer")]
//    public class CutsomerController : ControllerBase
//    {
//        private AppDbContext context;
//        private IConfiguration config;
//        private ILogger<AuthenticationController> logger;

//        public CutsomerController(AppDbContext context, IConfiguration config, ILogger<AuthenticationController> logger)
//        {
//            this.context = context;
//            this.config = config;
//            this.logger = logger;
//        }

//        [HttpGet]
//        [Route("getcustomers")]
//        public ActionResult<IEnumerable<Customer>> getCustomers()
//        {
//            var customers = context.customers.ToList();

//            return Ok(new { Customers = customers });

//        }


//    }

//}
