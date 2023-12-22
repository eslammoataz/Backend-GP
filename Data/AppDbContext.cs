using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Users;

using WebApplication1.Models.Entities.Users.ServiceProviders;


namespace WebApplication1.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Provider> provider { get; set; }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Criteria> Criterias { get; set; }

        public DbSet<Service> Services { get; set; }
        //public DbSet<ServiceRelation> SubServices { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<WorkerService> WorkerServices { get; set; }
        public DbSet<ProviderAvailability> ProviderAvailabilities { get; set; }
        public DbSet<TimeSlot>Slots { get; set; }   
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        //public DbSet<OrderDeletionRequest> OrderDeletionRequests { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the base User table
            builder.Entity<User>().ToTable("Users");

            builder.Entity<Provider>().ToTable("ServiceProviders");

            // Configure the Customer table
            builder.Entity<Customer>().ToTable("Customers");

            // Configure the ServiceProvider tables
            builder.Entity<Worker>().ToTable("Workers");
            builder.Entity<Consultant>().ToTable("Consultants");
            builder.Entity<Company>().ToTable("Companies");

            // Configure the Admin table
            builder.Entity<Admin>().ToTable("Admins");

            SeedOrderStatusData(builder);

            // Define your relationships here using Fluent API

            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);

            builder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.OrderStatusID);

            builder.Entity<ProviderAvailability>()
                .HasOne(wa => wa.ServiceProvider)
                .WithMany(w => w.Availabilities)
                .HasForeignKey(wa => wa.ServiceProviderID);

            builder.Entity<TimeSlot>()
                .HasOne(ts => ts.ProviderAvailability)
                .WithMany(pa => pa.Slots)
                .HasForeignKey(ts => ts.ProviderAvailabilityID);





            builder.Entity<Service>()
            .HasKey(s => s.ServiceID);


            builder.Entity<Service>()
           .HasOne(s => s.Criteria)
           .WithMany(s => s.Services)
           .HasForeignKey(s => s.CriteriaID)
           .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            //   builder.Entity<Service>()
            //.HasOne(s => s.Order)
            //.WithMany(o => o.Services)
            //.HasForeignKey(s => s.OrderID)
            //.OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            builder.Entity<Service>()
                .HasOne(s => s.ParentService)
                .WithMany(s => s.ChildServices)
                .HasForeignKey(s => s.ParentServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            builder.Entity<Service>()
                .HasMany(s => s.ChildServices)
                .WithOne(s => s.ParentService)
                .HasForeignKey(s => s.ParentServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior


            builder.Entity<WorkerService>()
       .HasKey(ws => new { ws.WorkerID, ws.ServiceID });

            builder.Entity<WorkerService>()
                .HasOne(ws => ws.Worker)
                .WithMany(w => w.WorkerServices)
                .HasForeignKey(ws => ws.WorkerID)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            builder.Entity<WorkerService>()
                .HasOne(ws => ws.Service)
                .WithMany(s => s.WorkerServices)
                .HasForeignKey(ws => ws.ServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior


            builder.Entity<UserReview>()
                .HasOne(ur => ur.Customer)
                .WithMany()
                .HasForeignKey(ur => ur.CustomerID);

            builder.Entity<UserReview>()
                .HasOne(ur => ur.Order)
                .WithMany()
                .HasForeignKey(ur => ur.OrderID);


        }


        private static void SeedOrderStatusData(ModelBuilder builder)
        {
            builder.Entity<OrderStatus>().HasData(
                 new OrderStatus
                 {
                     OrderStatusID = "1", // Adjust the ID as needed
                     StatusName = "Set"

                 }
             );
        }

    }
}
