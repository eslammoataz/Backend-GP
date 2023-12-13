using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.AuthRequestsValidations.Registers
{
    public class RegisterCompany : RegisterDto
    {
        [Required(ErrorMessage = "licence is Required")]
        public string license { get; set; }
        //upload sora lel license
    }
}
