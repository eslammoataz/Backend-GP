using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.AvailabilityRequestsValidations
{
    public class TimeSlotDto
    {

        [Required(ErrorMessage = "Start Time is Required ")]
        public TimeSpan StartTime { get; set; }

        [Compare(nameof(StartTime), ErrorMessage = "End time must be greater than start time.")]
        [Required(ErrorMessage = "End Time is Required ")]
        public TimeSpan EndTime { get; set; }
    }
}
