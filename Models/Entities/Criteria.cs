namespace WebApplication1.Models.Entities
{
    public class Criteria
    {
        public string CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        
        public string Description { get; set; }

        public List<Service> ?Services {  get; set; }

    }
}
