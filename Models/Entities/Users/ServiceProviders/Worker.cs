namespace WebApplication1.Models.Entities.Users.ServiceProviders
{
    public class Worker : ServiceProvider
    {
        public string NationalID { get; set; }
        public string CriminalRecord { get; set; }

        public List<WorkerAvailability> Availabilities { get; set; } // Make sure this property is present

        public List<WorkerService> WorkerServices { get; set; } = new List<WorkerService>();

        //image (fesh we tashbeh)

    }
}
