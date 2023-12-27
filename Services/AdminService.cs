using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Services;

public class AdminService :IAdminService
{
    private readonly UserManager<User> userManager;
    private readonly AppDbContext context;

    public AdminService(UserManager<User>userManager , AppDbContext context)
    {
        this.userManager = userManager;
        this.context = context;
    }
    public async Task<Response<List<object>>> GetAllServiceProviders()
    {
       var serviceProviders = context.Provider.Select(c => new
       {
           c.Id,
           c.FirstName,
           c.LastName,
           c.Email,
           c.isVerified
       }).ToList<object>();
       
       return new Response<List<object>>()
       {
           Status = "Success",
           Message = "Action Done Successfully",
           Payload = serviceProviders
       };
    }

    public async Task<Response<List<object>>> GetAllCustomers()
    {
        var customers = context.Customers.Select(c => new
        {
            c.Id,
            c.FirstName,
            c.LastName,
            c.Email
        }).ToList<object>();

        return new Response<List<object>>()
        {
            Status = "Success",
            Message = "Action Done Successfully",
            Payload = customers
        };
    }

    public async Task<Response<List<object>>> GetAllServices()
    {
        var services = context.Services.Include(s=>s.Criteria).Select(s => new
        {
            s.ServiceID,
            s.ServiceName,
            s.AvailabilityStatus,
            s.Criteria.CriteriaName
        }).ToList<object>();
        
        return new Response<List<object>>()
        {
            Status = "Success",
            Message = "Action Done Successfully",
            Payload = services
        };
    }
}