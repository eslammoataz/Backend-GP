using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Middlewares;


var builder = WebApplication.CreateBuilder(args);

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
builder.Logging.AddConsole();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwtIssuer,
//         ValidAudience = jwtIssuer,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//     };
// });


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "MyCookie";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

//Jwt configuration ends here

// allowing CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//// Add DbContext to the services
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
//);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

//app.Map("/api/customer", specificPathApp =>
//{
//    specificPathApp.UseMiddleware<AuthMiddleware>();
//});


/* 
The UseRouting method in ASP.NET Core is part of the middleware pipeline
and is responsible for setting up the routing system.
It plays a crucial role in handling incoming HTTP requests
and determining which endpoint should be executed based on the request's route.
*/

/*
 -From Microsoft Docs
UseRouting adds route matching to the middleware pipeline. This middleware looks at the set of endpoints defined in the app,
and selects the best match based on the request.

https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0
 */
app.UseRouting();

//app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/customer"), specificPathApp =>
//{
//    specificPathApp.UseMiddleware<AuthMiddleware>();
//    // Other middleware specific to /api/customer
//});

app.Map("/api/customer", customerApp =>
{

    customerApp.UseMiddleware<AuthMiddleware>();

    customerApp.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

});



//app.UseMiddleware<AuthMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
