using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Services
{
    public class WorkerServices : IWorkerServices
    {
        private readonly AppDbContext context;
        private readonly ILogger<WorkerServices> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private string workerId;
        private string serviceId;


        public WorkerServices(AppDbContext context, ILogger<WorkerServices> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }



        public async Task<Response> RegisterService(string workerId, string serviceId)
        {
            var user = await userManager.FindByIdAsync(workerId);

            var service = context.Services.SingleOrDefault(s => s.ServiceID == serviceId);

            if (user == null || !(user is Worker))
            {
                return new Response { Status = "Error", Message = "Worker Not Found" };
            }

            if (service == null)
            {
                return new Response { Status = "Error", Message = "Service Not Found" };
            }

            var worker = user as Worker;

            if (worker.WorkerServices.Any(ws => ws.ServiceID == serviceId))
            {
                return new Response { Status = "Error", Message = "Worker already registered for this service" };
            }


            var workerService = new WorkerService()
            {
                WorkerID = workerId,
                ServiceID = serviceId,
            };

            context.WorkerServices.Add(workerService);

            try
            {
                await context.SaveChangesAsync();
                return new Response { Status = "Success", Message = "Worker registered for Service" };


            }
            catch (Exception ex)
            {
                logger.LogError("|||---------------|||");
                logger.LogError(ex.Message);

                logger.LogInformation("|||---------------|||");
                logger.LogError(ex.InnerException.ToString());
                return new Response { Status = "Error", Message = "See the Logs" };
                ;
            }
        }

        public async Task<List<object>> GetRegisteredServices(string workerId)
        {
            var worker = await context.Workers
                .Include(w => w.WorkerServices)
                    .ThenInclude(ws => ws.Service)
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                return new List<object>();
            }

            var registeredServicesInfo = worker.WorkerServices
                .Select(ws => new
                {
                    ServiceID = ws.Service.ServiceID,
                    ServiceName = ws.Service.ServiceName
                })
                .ToList<object>();

            return registeredServicesInfo;
        }


    }
}
