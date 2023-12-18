using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Models.Entities;

public class UserReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string ReviewID { get; set; }
    public string CustomerID { get; set; }
    public string OrderID { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    [ForeignKey("CustomerID")]
    public Customer Customer { get; set; }

    [ForeignKey("OrderID")]
    public Order Order { get; set; }
}