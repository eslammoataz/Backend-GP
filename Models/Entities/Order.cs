using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Models.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string OrderID { get; set; }
    public string CustomerID { get; set; }
    public string OrderStatusID { get; set; }

    public decimal? TotalPrice {  get; set; }

    [ForeignKey("CustomerID")]
    public Customer Customer { get; set; }

    [ForeignKey("OrderStatusID")]
    public OrderStatus OrderStatus { get; set; }

    public List<WorkerService> WorkerServices { get; set; } = new List<WorkerService>();

    

    // public List<Service> Services { get; set; } = new List<Service>();
}