using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities.Users
{
    public abstract class User : IdentityUser
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
}
