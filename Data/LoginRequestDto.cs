using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class LoginRequestDto
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}