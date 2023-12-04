using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class RegistrationDto
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
