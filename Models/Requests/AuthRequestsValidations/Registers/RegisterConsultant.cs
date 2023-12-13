using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.AuthRequestsValidations.Registers
{
    public class RegisterConsultant : RegisterDto
    {

        [Required(ErrorMessage = "Salary is Required")]
        public decimal salary { get; set; }
    }
}
