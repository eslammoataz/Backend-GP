using WebApplication1.Models.Requests.ServiceRequestsValidation;
using WebApplication1.Models;

namespace WebApplication1.Services.Abstractions
{
    public interface ICustomerService
    {
        public  Task<Response<string>> RequestService(RequestServiceDto requestServiceDto, string customerId);

        public Task<Response<string>> CancelRequestService(string customerId, string requestId);

        public Task<Response<List<object>>> GetCustomerCart(string customerId);
    }
}
