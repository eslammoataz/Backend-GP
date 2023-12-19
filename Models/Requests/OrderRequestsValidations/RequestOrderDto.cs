using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.OrderRequestsValidations
{
    public class RequestOrderDto
    {


        [Required(ErrorMessage = "Service ID is Required to order")]
        public String ServiceID { get; set; }

        [Required(ErrorMessage = "Worker ID is Required to order")]
        public String WorkerID { get; set; }
    }
}
