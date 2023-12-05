//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Models;

//namespace WebApplication1.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public DbSet<Customer> customers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
//                .Build();

//            var connectionString = config.GetSection("constr").Value;

//            base.OnConfiguring(optionsBuilder);


//            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//        }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            //modelBuilder.Entity<Product>().ToTable("Products").HasKey("Id");
//            modelBuilder.Entity<Customer>().ToTable("Customers").HasKey("Id");

//        }
//    }
//}
