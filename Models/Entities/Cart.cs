using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Models.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CartID {  get; set; }


        public string CustomerID { get; set; }

        public List<ServiceRequest> ServiceRequests { get; set; }  
        
        //public List<ProviderService> WorkerServices = new List<ProviderService>();

        //public DateTime? AddedTime { get; set; }

        public DateTime? LastChangeTime { get; set; }


        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }


    }
}
