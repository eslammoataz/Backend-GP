using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Models.Entities;

public class ProviderAvailability
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string   ProviderAvailabilityID { get; set; }

    public string ServiceProviderID { get; set; }
    public string DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    [ForeignKey("ServiceProviderID")]
    public Provider ServiceProvider { get; set; }

}
