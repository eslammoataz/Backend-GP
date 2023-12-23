using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.ServiceRequestsValidation
{
    public class RequestServiceDto
    {
        [Required(ErrorMessage = "Provider ID is Required ")]
        public string providerId { get; set; }

        [Required(ErrorMessage = "Service ID is Required ")]
        public string serviceId { get; set; }

        public string scheduledHour { get; set; }

    }
}
