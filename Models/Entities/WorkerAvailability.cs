using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Models.Entities;

public class WorkerAvailability
{
    [Key]
    public string WorkerAvailabilityID { get; set; }
    public string WorkerID { get; set; }
    public string ServiceID { get; set; }
    public string DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    [ForeignKey("WorkerID")]
    public Worker Worker { get; set; }

    [ForeignKey("ServiceID")]
    public Service Service { get; set; }
}