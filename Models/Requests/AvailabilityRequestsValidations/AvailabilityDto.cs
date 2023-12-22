using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Requests.AvailabilityRequestsValidations
{
    public class AvailabilityDto
    {
        
        [Required(ErrorMessage = "DayOfWeek is Required ")]
        [EnumDataType(typeof(DaysOFTheWeek))]
        public DaysOFTheWeek DayOfWeek { get; set; }


        public DateTime? AvailabilityDate { get; set; }


        [Required(ErrorMessage = "Time slots is Required ")]
        public List<TimeSlotDto> Slots { get; set; } = new List<TimeSlotDto>();
    }
}
