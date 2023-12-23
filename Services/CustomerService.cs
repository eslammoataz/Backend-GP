using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Models.Requests.ServiceRequestsValidation;
using WebApplication1.Services.Abstractions;

namespace WebApplication1.Services
{
    public class CustomerService :ICustomerService
    {
        private readonly AppDbContext context;
        private readonly ILogger<ServiceProviderService> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private string workerId;
        private string serviceId;

        public CustomerService(AppDbContext context, ILogger<ServiceProviderService> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<Response<string>> RequestService(RequestServiceDto requestServiceDto, string customerId)
        {
            var provider = context.Provider.FirstOrDefault(p => p.Id == requestServiceDto.providerId);

            if (provider == null)
            {
                return new Response<string> { isError = true, Message = "Provider Not Found" };
            }
           var service = context.Services.FirstOrDefault(s => s.ServiceID== requestServiceDto.serviceId);
            if (service == null)
            {
                return new Response<string> { isError = true, Message = "Service Not Found" };

            }
            var workerService = provider.ProviderServices.FirstOrDefault(p => p.ProviderID == provider.Id);
            if (workerService == null)
            {
                return new Response<string> { isError = true, Message = "There is no Worker in this Service Not Found" };
            }

            var customer = context.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return new Response<string> { isError = true, Message = "Customer Not Found" };
            }

            TimeSpan scheduledHour= TimeSpan.Parse(requestServiceDto.scheduledHour);

            var slots = context.Provider
                .Where(p => p.Id == provider.Id)
                .SelectMany(p => p.Availabilities.SelectMany(a => a.Slots))
                .ToList();
            foreach ( var slot in slots ) {

               if(scheduledHour >= slot.StartTime && scheduledHour <= slot.EndTime)
                {
                    var providerAvailability = provider.Availabilities.FirstOrDefault(p => p.ServiceProviderID == provider.Id);


                    ///m7tagen showaia validation
                    var Firstslot = new TimeSlot
                    {

                        StartTime = slot.StartTime,
                        EndTime = scheduledHour.Add(TimeSpan.FromHours(0.5)),
                        enable = true,
                        ProviderAvailabilityID = providerAvailability.ProviderAvailabilityID,
                        ProviderAvailability = providerAvailability
                    };

                    var Secondslot = new TimeSlot
                    {

                        StartTime = scheduledHour.Add(TimeSpan.FromHours(1.5)),
                        EndTime = slot.EndTime,
                        enable = true,
                        ProviderAvailabilityID = providerAvailability.ProviderAvailabilityID,
                        ProviderAvailability = providerAvailability
                    };

                }
                else
                {
                    return new Response<string> { isError = true, Message = "The scheduled Hour is not Within the worker Availability" };

                }
            }
            var Cart = new Cart();

            var newRequest = new ServiceRequest
            {
                AddedTime=DateTime.Now,
         

            };

            newRequest.providerServices.Add(workerService);
            
            if (!string.IsNullOrEmpty(customer.CartID))
            {
                Cart=context.Carts.FirstOrDefault(c=>c.CartID == customer.CartID);  
                Cart.LastChangeTime = DateTime.Now;
                
            }
            else
            {

                Cart = new Cart
                {
    
                    LastChangeTime = DateTime.Now,
                    CustomerID = customerId,
                    Customer = customer,
                   
                };

            }

            newRequest.Cart = Cart;
            newRequest.CartID = Cart.CartID;
            Cart.ServiceRequests.Add(newRequest);
            context.SaveChanges();
            return new Response<string> { isError = false, Message = "Service Request is added succesfully to the cart" };

        }

        public async Task<Response<string>> CancelRequestService( string customerId, string requestId)
        {
            var customer = context.Customers
              .Include(c => c.Cart)
               .ThenInclude(ca => ca.ServiceRequests)
                .FirstOrDefault(p => p.Id == customerId);
            if (customer == null)
            {
                return new Response<string> { isError = true, Message = "Customer Not Found" };
            }

            var cart= context.Carts.FirstOrDefault(c=>customerId == c.CustomerID);
            if (cart == null)
            {
                return new Response<string> { isError = true, Message = " This Customer has no Cart" };
            }

            var request= context.ServiceRequests.FirstOrDefault(s=>s.ServiceRequestID == requestId);
            if (request == null)
            {
                return new Response<string> { isError = true, Message = " Request Not found" };

            }
            customer.Cart.ServiceRequests.Remove(request);
            context.SaveChanges();
            return new Response<string> { isError = false, Message = " Request is removed succesfully" };
   

        }
        public async Task<Response<List<object>>> GetCustomerCart(string customerId)
        {
            var customer = context.Customers
                  .Include(c => c.Cart)
                   .ThenInclude(ca=>ca.ServiceRequests)
                    .FirstOrDefault(p => p.Id == customerId);
            if (customer == null)
            {
                return new Response<List<object>>() { Payload = null, Message = "Customer is not found" };
            }

            var cart = context.Carts.FirstOrDefault(c => customerId == c.CustomerID);
            if (cart == null)
            {
                return new Response<List<object>>() { Payload = null, Message = "Cart is not found" };
            }

            List<object> response = new List<object>();
            response.Add(customer.Cart.ServiceRequests);

            return new Response<List<object>>() { Payload = response, Message = "Success" };
        }

    }
}
