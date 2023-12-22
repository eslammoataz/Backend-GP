using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities
{
    public class TimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TimeSlotID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSpan? Duration => EndTime - StartTime;

        public bool? enable {  get; set; }

         // Foreign key to relate TimeSlot to ProviderAvailability
        public string ProviderAvailabilityID { get; set; }

        [ForeignKey("ProviderAvailabilityID")]
        public ProviderAvailability ProviderAvailability { get; set; }


    }
}
