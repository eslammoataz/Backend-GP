using WebApplication1.Models;
using WebApplication1.Models.Requests.AvailabilityRequestsValidations;

namespace WebApplication1.Services
{
    public interface IServiceProviderService
    {
        Task<Response> RegisterService(string workerId, string serviceI);
        Task<List<object>> GetRegisteredServices(string workerId);

        Task<Response> AddAvailability(AvailabilityDto availabilityDto, string workerId);

        Task<List<object>> getAvailability(string workerId);


    }
}
