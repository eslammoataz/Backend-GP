using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IWorkerServices
    {
        Task<Response> RegisterService(string workerId, string serviceI);
        Task<List<object>> GetRegisteredServices(string workerId);
    }
}
