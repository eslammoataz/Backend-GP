//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;

//namespace WebApplication1.Middlewares
//{
//    public class AuthMiddleware
//    {
//        private readonly RequestDelegate next;
//        private readonly ILogger<AuthMiddleware> logger;

//        public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
//        {
//            this.next = next;
//            this.logger = logger;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Cookies["Auth"];
//            logger.LogInformation("context path: " + context.Request.Path);

//            if (string.IsNullOrEmpty(token))
//            {
//                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                await context.Response.WriteAsync("Unauthorized: Missing or invalid token");
//                return;
//            }

//            var handler = new JwtSecurityTokenHandler();
//            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

//            if (jsonToken == null)
//            {
//                context.Response.StatusCode = StatusCodes.Status404NotFound;
//                await context.Response.WriteAsync("Invalid Token");
//                return;
//            }

//            // Retrieve claims from the JWT
//            var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;


//            // Create a ClaimsIdentity with the user's claims
//            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }, "custom", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

//            // Set the ClaimsPrincipal for the current context.User
//            context.User = new ClaimsPrincipal(identity);

//            Console.WriteLine($"User ID from Token: {userId}");

//            // Continue the request pipeline
//            await next(context);
//        }

//    }
//}
