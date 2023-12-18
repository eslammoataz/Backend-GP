using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.ServiceRequestsValidation
{
    public class AddCriteriaDto
    {

        [Required(ErrorMessage = "Criteria Name is Required ")]
        public string CriteriaName { get; set; }

        [Required(ErrorMessage = "Criteria Description is Required ")]
        public string Description { get; set; }
    }
}
