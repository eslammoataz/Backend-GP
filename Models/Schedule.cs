using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Schedule
{
    public string ScheduleID { get; set; }
    public string ServiceID { get; set; }
    public string DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    [ForeignKey("ServiceID")]
    public Service Service { get; set; }
}