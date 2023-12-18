using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.AuthRequests;

namespace WebApplication1.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> adminManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly ILogger<AdminController> logger;

        public AdminController(AppDbContext _context, UserManager<User> _adminManager, RoleManager<IdentityRole> _roleManager, IConfiguration _config, ILogger<AdminController> _logger)
        {
            context = _context;
            adminManager = _adminManager;
            roleManager = _roleManager;
            config = _config;
            logger = _logger;
        }



    }
}
