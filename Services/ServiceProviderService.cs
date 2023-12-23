﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;
using WebApplication1.Models.Requests.AvailabilityRequestsValidations;

namespace WebApplication1.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly AppDbContext context;
        private readonly ILogger<ServiceProviderService> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private string workerId;
        private string serviceId;


        public ServiceProviderService(AppDbContext context, ILogger<ServiceProviderService> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }



        public async Task<Response<string>> RegisterService(string workerId, string serviceId)
        {
            var user = await userManager.FindByIdAsync(workerId);

            var service = context.Services.SingleOrDefault(s => s.ServiceID == serviceId);

            if (user == null || !(user is Worker))
            {
                return new Response<string> { isError = true, Message = "Worker Not Found" };
            }

            if (service == null)
            {
                return new Response<string> { isError = true, Message = "Service Not Found" };
            }

            var worker = user as Worker;

            if (worker.ProviderServices.Any(ws => ws.ServiceID == serviceId))
            {
                return new Response<string> { isError = true, Message = "Worker already registered for this service" };
            }


            var workerService = new ProviderService()
            {
                ProviderID = workerId,
                ServiceID = serviceId,
            };

            context.ProviderServices.Add(workerService);

            await context.SaveChangesAsync();
            return new Response<string> { Message = "Worker registered for Service" };
        }


        public async Task<Response<List<object>>> GetRegisteredServices(string workerId)
        {
            var worker = await context.Workers
                .Include(w => w.ProviderServices)
                    .ThenInclude(ws => ws.Service)
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                return new Response<List<object>>() { isError = true, Message = "Worker Not Found" };
            }

            var registeredServicesInfo = worker.ProviderServices
                .Select(ws => new
                {
                    ServiceID = ws.Service.ServiceID,
                    ServiceName = ws.Service.ServiceName
                })
                .ToList<object>();

            return new Response<List<object>>() { Payload = registeredServicesInfo, Message = "Success" };
        }

        public async Task<Response<string>> AddAvailability(AvailabilityDto availabilityDto, string providerId)
        {
            var provider = context.Provider.FirstOrDefault(w => w.Id == providerId);

            if (provider == null)
            {
                return new Response<string> { isError = true, Message = "worker Id is not valid" };
            }


            var availability = new ProviderAvailability
            {
                ServiceProviderID = workerId,
                DayOfWeek = availabilityDto.DayOfWeek,
                AvailabilityDate = DateTime.Now,
                ServiceProvider = provider
            };


            List<TimeSlot> timeSlots = await ConverttoTimeSlot(availabilityDto.Slots, availability);
            availability.Slots = timeSlots;


            context.ProviderAvailabilities.Add(availability);

            provider.Availabilities.Add(availability);

            context.SaveChanges();

            return new Response<string> { Message = "Availability Added Successfully" };
        }


        public async Task<Response<List<object>>> getAvailability(string providerId)
        {
            var provider = context.Provider
                             .Include(p => p.Availabilities)
                              .ThenInclude(a => a.Slots)
                               .FirstOrDefault(p => p.Id == providerId);

            if (provider == null)
            {
                return null;
            }

            List<object> availability = new List<object>();

            var avails = provider.Availabilities
                .SelectMany(a => a.Slots)
                .Select(slot => new
                {
                    slot.StartTime,
                    slot.EndTime
                })
                .ToList<object>();


            List<object> reponse = new List<object>();

            foreach (var providerAvail in provider.Availabilities)
            {

                List<object> slots = new List<object>();

                foreach (var avail in providerAvail.Slots)
                {
                    slots.Add(new
                    {
                        StartTime = avail.StartTime,
                        EndTime = avail.EndTime
                    });

                }

                var obj = new
                {
                    DayOfWeek = providerAvail.DayOfWeek,
                    Date = providerAvail.AvailabilityDate,
                    Slots = slots
                };
                reponse.Add(obj);
            }

            return new Response<List<object>>() { Payload = reponse, Message = "Success" };
        }




        public async Task<List<TimeSlot>> ConverttoTimeSlot(List<TimeRange> timeRanges, ProviderAvailability providerAvailability)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            foreach (var timeRange in timeRanges)
            {
                var timeslot = new TimeSlot
                {
                    StartTime = TimeSpan.Parse(timeRange.startTime),
                    EndTime = TimeSpan.Parse(timeRange.endTime),
                    enable = true,
                    ProviderAvailabilityID = providerAvailability.ProviderAvailabilityID,
                    ProviderAvailability = providerAvailability
                };
                timeSlots.Add(timeslot);
            }
            return timeSlots;
        }



    }
}
