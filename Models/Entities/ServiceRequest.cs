using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
    public class ServiceRequest

    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceRequestID { get; set; }

        public string CartID { get; set; }

        [ForeignKey("CartID")]
        public Cart Cart { get; set; }

        public ProviderService providerService { get; set; }
        public DateTime? AddedTime { get; set; }

    }
}
