using WebApplication1.Models;
using WebApplication1.Models.Requests.AvailabilityRequestsValidations;

namespace WebApplication1.Services
{
    public interface IServiceProviderService
    {
        Task<Response<string>> RegisterService(string workerId, string serviceId, decimal Price);
        Task<Response<List<object>>> GetRegisteredServices(string workerId);

        Task<Response<string>> AddAvailability(AvailabilityDto availabilityDto, string workerId);

        Task<Response<List<object>>> getAvailability(string workerId);

        Task<Response<Object>> ShowOrderDetails(string orderId);
        
        Task<Response<List<object>>> ApproveOrder( string orderId);
        Task<Response<List<object>>> RejectOrder( string orderId);
        Task<Response<Object>> CancelOrder(string orderId);
        Task<Response<Object>> GetAllServiceProviders();
    }
}
