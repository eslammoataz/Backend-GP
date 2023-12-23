﻿using System.Security.Claims;
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
using WebApplication1.Models.Requests.AuthRequestsValidations.Registers;
using Microsoft.AspNetCore.Routing;
using WebApplication1.Models.Emails;
using WebApplication1.ServicesWebApplication1.Services;
using WebApplication1.Services.EmailService;
using System;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Controllers.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly ILogger<AdminController> logger;
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;


        public AdminController(AppDbContext _context, UserManager<User> _adminManager, RoleManager<IdentityRole> _roleManager, IConfiguration _config, ILogger<AdminController> _logger, IEmailService _emailService)
        {
            context = _context;
            userManager = _adminManager;
            roleManager = _roleManager;
            config = _config;
            logger = _logger;
            emailService = _emailService;

        }

        [HttpGet("getRequests")]
        public IActionResult GetRequests()
        {
            var unHandeledProviders = context.Provider.Where(w => w.isVerified==false).ToList();

            if (unHandeledProviders.Count == 0)
            {
                return NotFound("No Requests found");
            }
            else
            {
                return Ok(unHandeledProviders);
            }


        }


        [HttpPost]
        [Route("ApproveServiceProvider")]
        public async Task<IActionResult> ApproveServiceProviderRegister(string WorkerID)
        {

            var provider = context.Provider.FirstOrDefault(w => w.Id == WorkerID);
            if (provider == null)
            {
                return BadRequest("Wrong Worker ID");
            }

            provider.isVerified = true;
            context.SaveChanges();

            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(provider);

            logger.LogInformation(provider.Email);

            var message = new EmailDto(provider.Email!, "Sarvicny: Worker Approved Successfully", "Congratulations you are accepted");

            emailService.SendEmail(message);

            var success= new Response { Status = "Success", Message = $"Worker Approved Successfully , Verification Email sent to {provider.Email} " };
            return Ok(success);

        }


        [HttpPost]
        [Route("RejectServiceProvider")]
        public async Task<IActionResult> RejectServiceProviderRegister(string WorkerID)
        {

            var provider = context.Provider.FirstOrDefault(w => w.Id == WorkerID&& w.isVerified==false);
            if (provider == null)
            {
                return BadRequest("Wrong Worker ID");
            }

           
            context.Provider.Remove(provider);
            context.SaveChanges();

            //Add Token to Verify the email....
            var token = await userManager.GenerateEmailConfirmationTokenAsync(provider);


            var message = new EmailDto(provider.Email!, "Sarvicny: Worker Rejected", "Sorry you are rejected" );

            emailService.SendEmail(message);

            var success = new Response { Status = "Success", Message = $" Worker Rejected Successfully, Verification Email sent to {provider.Email} " };
            return Ok(success);

        }

    }
}

