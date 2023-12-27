using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IAdminService
{
     public Task<Response<List<object>>> GetAllServiceProviders();
     public Task<Response<List<object>>> GetAllCustomers();
     
     public Task<Response<List<object>>> GetAllServices();
}