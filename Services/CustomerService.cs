using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.ServiceRequestsValidation;
using WebApplication1.Services.Abstractions;

namespace WebApplication1.Services
{
    public class CustomerService : ICustomerService
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
            var provider = context.Provider.Include(p => p.Availabilities).Include(w => w.ProviderServices).FirstOrDefault(p => p.Id == requestServiceDto.providerId);

            if (provider == null)
            {
                return new Response<string> { isError = true, Message = "Provider Not Found" };
            }
            var service = context.Services.FirstOrDefault(s => s.ServiceID == requestServiceDto.serviceId);
            if (service == null)
            {
                return new Response<string> { isError = true, Message = "Service Not Found" };

            }
            var workerService = provider.ProviderServices
                .FirstOrDefault(p => p.ProviderID == provider.Id);

            if (workerService == null)
            {
                return new Response<string> { isError = true, Message = "There is no Worker in this Service Not Found" };
            }

            var customer = context.Customers
            .Include(c => c.Cart)
            .ThenInclude(c => c.ServiceRequests)
            .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return new Response<string> { isError = true, Message = "Customer Not Found" };
            }

            TimeSpan scheduledHour = TimeSpan.Parse(requestServiceDto.scheduledHour);

            var slots = context.Provider
                .Where(p => p.Id == provider.Id)
                .SelectMany(p => p.Availabilities.SelectMany(a => a.Slots))
                .ToList();

            foreach (var slot in slots)
            {

                if (scheduledHour >= slot.StartTime && scheduledHour.Add(TimeSpan.FromHours(1)) <= slot.EndTime && slot.enable == true)
                {
                    var providerAvailability = provider.Availabilities.FirstOrDefault(p => p.ServiceProviderID == provider.Id);
                    var Firstslot = new TimeSlot
                    {
                        enable = true,
                        ProviderAvailabilityID = providerAvailability.ProviderAvailabilityID,
                        ProviderAvailability = providerAvailability
                    };
                            
                    var Secondslot = new TimeSlot {

                        enable = true,
                        ProviderAvailabilityID = providerAvailability.ProviderAvailabilityID,
                        ProviderAvailability = providerAvailability

                    };

                    if (slot.StartTime == scheduledHour)
                    {
                       
                        Secondslot.StartTime = scheduledHour.Add(TimeSpan.FromHours(1));
                        Secondslot.EndTime = slot.EndTime;
                        providerAvailability.Slots.Add(Secondslot);

                    }
                    
                    else {
                        Firstslot.StartTime = slot.StartTime;
                        Firstslot.EndTime = scheduledHour.Subtract(TimeSpan.FromHours(0.5));
                       
                        Secondslot.StartTime = scheduledHour.Add(TimeSpan.FromHours(1.5));
                        Secondslot.EndTime = slot.EndTime;

                        providerAvailability.Slots.Add(Firstslot);
                        providerAvailability.Slots.Add(Secondslot);
                    }
        
                    slot.enable = false;


                    context.SaveChanges();
                    break;

                }
                else
                {
                    return new Response<string> { isError = true, Message = "The scheduled Hour is not Within the worker Availability" };

                }
            }

            var newRequest = new ServiceRequest
            {
                AddedTime = DateTime.Now,
                providerService = workerService
            };

            if (customer.Cart == null)
            {
                var Cart = new Cart
                {
                    Customer = customer,
                    CustomerID = customer.Id,
                    LastChangeTime = DateTime.Now
                };
                customer.Cart = Cart;

            }
            else
            {
                customer.Cart.CustomerID= customer.Id;
                customer.Cart.Customer= customer;
                customer.Cart.LastChangeTime = DateTime.Now;
            }

       
            newRequest.Cart = customer.Cart;
            newRequest.CartID = customer.Cart.CartID;
            customer.Cart.ServiceRequests.Add(newRequest);
            context.SaveChanges();
            return new Response<string> { isError = false, Message = "Service Request is added succesfully to the cart" };

        }

        public async Task<Response<string>> CancelRequestService(string customerId, string requestId)
        {
            var customer = context.Customers
              .Include(c => c.Cart)
               .ThenInclude(ca => ca.ServiceRequests)
                .FirstOrDefault(p => p.Id == customerId);
            if (customer == null)
            {
                return new Response<string> { isError = true, Message = "Customer Not Found" };
            }

            var cart = context.Carts.FirstOrDefault(c => customerId == c.CustomerID);
            if (cart == null)
            {
                return new Response<string> { isError = true, Message = " This Customer has no Cart" };
            }

            var request = context.ServiceRequests.FirstOrDefault(s => s.ServiceRequestID == requestId);
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
                   .ThenInclude(ca => ca.ServiceRequests).
                   ThenInclude(s=>s.providerService)
                    .FirstOrDefault(p => p.Id == customerId);
            if (customer == null)
            {
                return new Response<List<object>>() { Payload = null, Message = "Customer is not found" };
            }

            var cart = customer.Cart;
            if (cart == null)
            {
                return new Response<List<object>>() { Payload = null, Message = "Cart is not found" };
            }



            var response = customer.Cart.ServiceRequests.Select(s => new 
            {
                s.AddedTime,
                s.providerService.ServiceID,
                s.providerService.ProviderID
            }).ToList<object>();
           

            return new Response<List<object>>() { Payload = response, Message = "Success" };
        }

        public async Task<Response<string>> OrderService(string customerId)
        {
            var customer = context.Customers
                .Include(c => c.Cart)
                .ThenInclude(c => c.ServiceRequests)
                .ThenInclude(s => s.providerService)
                .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return new Response<string> { isError = true, Message = "Customer Not Found" };
            }
            
            var Order = new Order()
            {
                Customer = customer,
                CustomerID = customerId,
                OrderStatusID = "1", //value (status name)=set
                OrderStatus=context.OrderStatuses.FirstOrDefault(o=>o.OrderStatusID=="1"),
            };

            var totalPrice = customer.Cart.ServiceRequests.Sum(s => s.providerService.Price);

            Order.TotalPrice = totalPrice;
            context.Orders.Add(Order);
            context.SaveChanges();
            return new Response<string> { isError = false, Message = "Order is setted sucessfully" };


        }

    }

}
