using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.AuthRequestsValidations.Registers
{
    public class RegisterCustomer : RegisterDto
    {

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is Required")]
        public string PhoneNumber { get; set; }



    }
}