using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Models.Entities;

public class ProviderService
{
    public string ProviderID { get; set; }
    public string ServiceID { get; set; }

    public decimal Price { get; set; }

    [ForeignKey("ProviderID")]
    public Provider Provider { get; set; }

    [ForeignKey("ServiceID")]
    public Service Service { get; set; }

    public List<ServiceRequest> ServiceRequest { get; set; }
}