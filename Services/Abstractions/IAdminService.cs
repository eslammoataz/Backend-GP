using WebApplication1.Models;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Services;

public interface IAdminService
{
     public Task<Response<List<object>>> GetAllServiceProviders();
     public Task<Response<List<object>>> GetAllCustomers();
     
     public Task<Response<List<object>>> GetAllServices();

    public Task<Response<Provider>> ApproveServiceProviderRegister(string WorkerID);



}