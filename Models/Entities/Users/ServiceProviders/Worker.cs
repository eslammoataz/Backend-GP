namespace WebApplication1.Models.Entities.Users.ServiceProviders
{
    public class Worker : Provider
    {
        public string NationalID { get; set; }
        public string CriminalRecord { get; set; }



        public List<WorkerService> WorkerServices { get; set; } = new List<WorkerService>();

        //image (fesh we tashbeh)

    }
}
