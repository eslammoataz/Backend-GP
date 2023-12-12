using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterCustomer
    {

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }


        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is Required")]
        public string PhoneNumber { get; set; }



    }
}