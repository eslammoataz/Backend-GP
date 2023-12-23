using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Models.Entities
{
    public class Cart
    {
        public Cart()
        {
            ServiceRequests = new List<ServiceRequest>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CartID { get; set; }


        public string CustomerID { get; set; }

        public List<ServiceRequest> ServiceRequests { get; set; }

        //public List<ProviderService> WorkerServices = new List<ProviderService>();

        //public DateTime? AddedTime { get; set; }

        public DateTime? LastChangeTime { get; set; }


        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }


    }
}
