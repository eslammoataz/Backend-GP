//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using WebApplication1.Data;
//using WebApplication1.Helpers;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    [ApiController]
//    [Route("api/auth")]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly AppDbContext context;
//        private readonly IConfiguration config;
//        private readonly ILogger<AuthenticationController> logger;

//        public AuthenticationController(AppDbContext context, IConfiguration config, ILogger<AuthenticationController> logger)
//        {
//            this.context = context;
//            this.config = config;
//            this.logger = logger;
//        }

//        [HttpPost]
//        [Route("register")]
//        public IActionResult register(RegistrationDto registrationDto)
//        {

//            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registrationDto.Password, 13);

//            // Your registration logic
//            var customer = new Customer()
//            {
//                email = registrationDto.email,
//                FirstName = registrationDto.FirstName,
//                LastName = registrationDto.LastName,
//                Password = hashedPassword,
//            };

//            context.customers.Add(customer);

//            context.SaveChanges();


//            var tokenString = JWT.generateToken(customer.Id.ToString(), config);
//            // Set the token in the response header
//            HttpContext.Response.Headers.Add("Authorization", "Bearer " + tokenString);

//            // Set the token in a cookie
//            Response.Cookies.Append("Auth", tokenString, new CookieOptions
//            {
//                HttpOnly = true,
//                Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
//                SameSite = SameSiteMode.Strict,
//                Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
//            });


//            return Ok("User Registered Successfully");
//        }

//        [HttpPost]
//        [Route("login")]
//        public IActionResult login(LoginRequestDto loginRequest)
//        {
//            var customer = context.customers.FirstOrDefault(c => c.email == loginRequest.email);

//            if (customer == null)
//            {
//                return NotFound("User not found");
//            }

//            var match = BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.password, customer.Password);


//            if (match)
//            {
//                var tokenString = JWT.generateToken(customer.Id.ToString(), config);

//                // Set the token in the response header
//                Response.Headers.Add("Authorization", "Bearer " + tokenString);

//                // Set the token in a cookie
//                Response.Cookies.Append("Auth", tokenString, new CookieOptions
//                {
//                    HttpOnly = true,
//                    Secure = true, // Use "true" in production to ensure the cookie is only sent over HTTPS
//                    SameSite = SameSiteMode.Strict,
//                    Expires = DateTime.Now.AddMinutes(120) // Set the cookie expiration time
//                });

//                return Ok(new { Message = "Logged in successfully", Token = tokenString });
//            }

//            return Unauthorized("Invalid credentials");
//        }



//    }

//}
