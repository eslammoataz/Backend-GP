using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.AuthRequestsValidations.Registers
{
    public class RegisterCustomer : RegisterDto
    {

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

    }
}