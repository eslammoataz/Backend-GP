using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Models.Entities;

public class ProviderAvailability
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string ProviderAvailabilityID { get; set; }

    public string ServiceProviderID { get; set; }

    // Date or week associated with the availability data
    //to be able to reserve and retrieve previous data
    public DateTime? AvailabilityDate { get; set; }

    public string DayOfWeek { get; set; }


    public List<TimeSlot> Slots { get; set; } = new List<TimeSlot>();

    [ForeignKey("ServiceProviderID")]
    public Provider ServiceProvider { get; set; }

}
