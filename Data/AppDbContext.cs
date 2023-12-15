using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Models.Entities.Users.ServiceProviders;

namespace WebApplication1.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the base User table
            builder.Entity<User>().ToTable("Users");

            // Configure the Customer table
            builder.Entity<Customer>().ToTable("Customers");

            // Configure the ServiceProvider tables
            builder.Entity<Worker>().ToTable("Workers");
            builder.Entity<Consultant>().ToTable("Consultants");
            builder.Entity<Company>().ToTable("Companies");

            // Configure the Admin table
            builder.Entity<Admin>().ToTable("Admins");

            SeedRoles(builder);
            //SeedAdminData(builder);

        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "CUSTOMER" },
                new IdentityRole() { Name = "Worker", ConcurrencyStamp = "1", NormalizedName = "WORKER" }
                );

        }

        private static void SeedAdminData(ModelBuilder builder)
        {
            builder.Entity<Admin>().HasData(
                 new Admin
                 {
                     Id = "1", // Adjust the ID as needed
                     UserName = "admin@example.com",
                     NormalizedUserName = "ADMIN@EXAMPLE.COM",
                     Email = "admin@example.com",
                     NormalizedEmail = "ADMIN@EXAMPLE.COM",
                     EmailConfirmed = true,
                     PasswordHash = "admin", // Hash the password using a secure hashing algorithm
                     SecurityStamp = string.Empty,
                 }
             );
        }
    }
}
