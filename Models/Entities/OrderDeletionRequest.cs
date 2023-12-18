//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Models.Entities.Users;

//namespace WebApplication1.Models;

//public class OrderDeletionRequest
//{
//    public string OrderDeletionRequestID { get; set; }
//    public string OrderID { get; set; }
//    public string CustomerID { get; set; }
//    public string DeletionReason { get; set; }
//    public string RequestStatus { get; set; }

//    [ForeignKey("OrderID")]
//    public Order Order { get; set; }

//    [ForeignKey("CustomerID")]
//    public Customer Customer { get; set; }
//}