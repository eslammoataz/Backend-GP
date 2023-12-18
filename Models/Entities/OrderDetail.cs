//using System.ComponentModel.DataAnnotations.Schema;

//namespace WebApplication1.Models;

//public class OrderDetail
//{
//    public string OrderDetailID { get; set; }
//    public string OrderID { get; set; }
//    public string ServiceID { get; set; }
//    public int Quantity { get; set; }
//    public decimal TotalPrice { get; set; }

//    [ForeignKey("OrderID")]
//    public Order Order { get; set; }

//    [ForeignKey("ServiceID")]
//    public Service Service { get; set; }

//}