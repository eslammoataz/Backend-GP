using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Services;

public class ServicesService: IServicesService
{
    private readonly AppDbContext context;
    private readonly UserManager<User> userManager;

    public ServicesService(AppDbContext context , UserManager<User>userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }
    public async Task<Response<List<object>>> GetAllServices(string CriteriaID)
    {
        var criteria = context.Criterias.Include(c =>c.Services)
            .FirstOrDefault(c => c.CriteriaID == CriteriaID);

        if (criteria is null)
        {
            return new Response<List<object>>
            {
                Status = "Error" ,
                isError = true,
                Message = "Action Not Done",
                Errors = new List<string> { "Criteria Not Found" }
            };
        }

        List<object> services = new List<object>(criteria.Services.Select(s => new
        {
            s.ServiceID,
            s.ServiceName,
            s.AvailabilityStatus
        }));

        return new Response<List<object>>
        {
            Status = "Success" ,
            Message = "Action Done Successfully",
            Payload = services
        };
    }

    public async Task<Response<List<object>>> GetAllWorkers(string serviceId)
    {
        var service = context.Services.Include(s => s.ProviderServices)
            .ThenInclude(ps => ps.Provider)
            .FirstOrDefault(s => s.ServiceID == serviceId);

        if (service is null)
        {
            return new Response<List<object>>()
            {
                isError = true,
                Status = "Error",
                Message = "Action Not Done",
                Errors = new List<string> { "Service Not Found" }
            };
        }

        var providers = service.ProviderServices.Select(ps => ps.Provider).Select(p => new
        {
            p.Id,
            p.FirstName,
            p.LastName,

        }).ToList<object>();
        
        return new Response<List<object>>()
        {
            Status = "Success",
            Message = "Action Done Successfully",
            Payload = providers
        };

    }
}