using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Models.Entities;

public class WorkerService
{
    public string WorkerID { get; set; }
    public string ServiceID { get; set; }

    [ForeignKey("WorkerID")]
    public Worker Worker { get; set; }

    [ForeignKey("ServiceID")]
    public Service Service { get; set; }
}