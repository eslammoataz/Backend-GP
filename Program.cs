using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Middlewares;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Services;
using WebApplication1.Services.Abstractions;
using WebApplication1.Services.EmailService;
using WebApplication1.ServicesWebApplication1.Services;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Logging.AddConsole();

        // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //  .AddJwtBearer(options =>
        //  {
        //      options.TokenValidationParameters = new TokenValidationParameters
        //      {
        //          ValidateIssuer = true,
        //          ValidateAudience = true,
        //          ValidateLifetime = true,
        //          ValidateIssuerSigningKey = true,
        //          ValidIssuer = jwtIssuer,
        //          ValidAudience = jwtIssuer,
        //          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        //      };
        //  });


        // builder.Services.AddAuthentication(options =>
        // {
        //     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // })
        //Registering Identity 
        var Configuration = builder.Configuration;
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                string jwtIssuer = Configuration.GetSection("Jwt:Issuer").Value; 
                string jwtKey = Configuration.GetSection("Jwt:Key").Value;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            }) .AddCookie(options =>
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
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithExposedHeaders("x-Authorization") // Expose custom headers if needed
                        .SetIsOriginAllowed(_ => true)
                    .WithHeaders("Content-Type"); // Allow Content-Type header
                });
        });




        builder.Services.AddSingleton(Configuration);

        var connectionString = Configuration.GetSection("constr").Value;

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );


        builder.Services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = false);

        builder.Services.AddHttpContextAccessor();


        builder.Services.AddScoped<IEmailConfirmService, EmailConfirmService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IServiceProviderService, ServiceProviderService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IServicesService, ServicesService>();
        builder.Services.AddScoped<IAdminService, AdminService>();

        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


        // Configure the Interfaces for the Identity
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>() // this adds the implementation of the interfaces
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(); // UserManager / SigninManager / RoleManager

        //End of Identity 





        // Add services to the container.
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        var app = builder.Build();

        //Ensures that the database is created and all migrations are applied
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var Services = scope.ServiceProvider;
                var userManager = Services.GetRequiredService<UserManager<User>>();
                var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = Services.GetRequiredService<AppDbContext>();
                var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

                context.Database.EnsureCreated();

                // Check if any migrations are pending
                if (context.Database.GetPendingMigrations().Any())
                {
                    //dbContext.Database.Migrate(); // Apply pending migrations
                }

                await AppDbContextSeed.SeedData(userManager, roleManager, context);
            }
            catch (Exception ex)
            {
                var LoggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var Logger = LoggerFactory.CreateLogger<Program>(); // Creating a logger for the Program class
                Logger.LogError(ex, ex.Message); // Logging the error
            }
        }


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");


        app.UseAuthentication();


        //to register middlewares

        //app.Map("/api/auth", customerApp =>
        //{
        //    customerApp.UseRouting();

        //    customerApp.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //});

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.MapControllers();

        app.Run();
    }
}