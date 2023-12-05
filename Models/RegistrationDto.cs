using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegistrationDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string email { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
