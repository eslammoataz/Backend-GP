using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests
{
    public class AvailabilityDto
    {

        [Required(ErrorMessage = "DayOfWeek is Required ")]
        public string DayOfWeek { get; set; }
        [Required(ErrorMessage = "Start time is Required ")]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "End time is Required ")]
        public TimeSpan EndTime { get; set; }
    }
}
