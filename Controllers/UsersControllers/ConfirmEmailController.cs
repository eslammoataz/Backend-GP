using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Services.EmailService;
using static WebApplication1.Services.EmailService.IEmailConfirmService;

namespace WebApplication1.Controllers.UsersControllers
{

    public class ConfirmEmailController : ControllerBase
    {
        private readonly IEmailConfirmService emailConfirm;
        public ConfirmEmailController(IEmailConfirmService emailConfirm)
        {
            this.emailConfirm = emailConfirm;
    
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> Confirm(string token, string email)
        {
            var isEmailConfirmed = await emailConfirm.ConfirmEmailAsync(token, email);

            if (isEmailConfirmed)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Email Verified Successfully" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "This User Does not exist!" });
        }
    }
}
