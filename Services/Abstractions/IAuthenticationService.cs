using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.AuthRequests;

namespace WebApplication1.Services
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Register(User user, string role, string password);

        Task<Response<Object>> Login(LoginRequestDto loginRequestDto);
    }
    
}
