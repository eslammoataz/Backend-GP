using WebApplication1.Models.Entities.Users.ServiceProviders;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string ServiceID { get; set; }
    public string ServiceName { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string AvailabilityStatus { get; set; }


    // Explicit foreign key for the Criteria relationship
    public string? CriteriaID { get; set; }

    // Navigation property for the Criteria relationship
    public Criteria? Criteria { get; set; }

    // Explicit foreign key for the Order relationship
    public string? OrderID { get; set; }

    // Navigation property for the Order relationship
    public Order? Order { get; set; }

    // Foreign key for self-referencing relationship
    public string? ParentServiceID { get; set; }

    // Navigation property for the parent service
    public Service? ParentService { get; set; }

    // Navigation property for the child services
    public List<Service> ChildServices { get; set; } = new List<Service>();

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    public List<Worker> Workers { get; set; } = new List<Worker>();
    public List<WorkerService> WorkerServices { get; set; } = new List<WorkerService>();
}