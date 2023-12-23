namespace WebApplication1.Models.Entities.Users.ServiceProviders
{
    public abstract class Provider : User
    {
        public Provider()
        {
            Availabilities = new List<ProviderAvailability>();
        }
        public bool isVerified { get; set; }
        public List<ProviderAvailability> Availabilities { get; set; } // Make sure this property is present
        //list<Service>
        //image (profile)
        //image (license)
    }
}
