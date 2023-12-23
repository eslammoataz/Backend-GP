using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities
{
    public class ServiceRequest

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceRequestID {  get; set; }

        public string CartID {  get; set; }

        [ForeignKey("CartID")]
        public Cart Cart { get; set; }

        public List<ProviderService> providerServices { get; set; }=new List<ProviderService>();
        public DateTime? AddedTime { get; set; }

    }
}
