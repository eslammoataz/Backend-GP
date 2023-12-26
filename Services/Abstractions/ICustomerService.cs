using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Requests.ServiceRequestsValidation;

namespace WebApplication1.Services.Abstractions
{
    public interface ICustomerService
    {
        public Task<Response<string>> RequestService(RequestServiceDto requestServiceDto, string customerId);

        public Task<Response<string>> CancelRequestService(string customerId, string requestId);

        public Task<Response<List<object>>> GetCustomerCart(string customerId);

        public Task<Response<string>> OrderService(string customerId);
    }
}
