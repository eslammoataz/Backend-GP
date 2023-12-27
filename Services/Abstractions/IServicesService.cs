using WebApplication1.Models;
using WebApplication1.Models.Requests.ServiceRequestsValidation;

namespace WebApplication1.Services;

public interface IServicesService
{
    public Task<Response<List<object>>> GetAllServices(string criteriaName);
    public Task<Response<List<object>>> GetAllWorkers(string serviceId);
}