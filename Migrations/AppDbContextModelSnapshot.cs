﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Service", b =>
                {
                    b.Property<string>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CriteriaID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ParentServiceID")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ServiceID");

                    b.HasIndex("CriteriaID");

                    b.HasIndex("ParentServiceID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cart", b =>
                {
                    b.Property<string>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastChangeTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CartID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Criteria", b =>
                {
                    b.Property<string>("CriteriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CriteriaName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CriteriaID");

                    b.ToTable("Criterias");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Order", b =>
                {
                    b.Property<string>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OrderStatusID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("OrderStatusID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.OrderStatus", b =>
                {
                    b.Property<string>("OrderStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("OrderStatusID");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            OrderStatusID = "1",
                            StatusName = "Set"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderAvailability", b =>
                {
                    b.Property<string>("ProviderAvailabilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("AvailabilityDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ServiceProviderID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ProviderAvailabilityID");

                    b.HasIndex("ServiceProviderID");

                    b.ToTable("ProviderAvailabilities");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderService", b =>
                {
                    b.Property<string>("ProviderID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ServiceID")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("ProviderID", "ServiceID");

                    b.HasIndex("ServiceID");

                    b.ToTable("ProviderServices");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Schedule", b =>
                {
                    b.Property<string>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("ServiceID")
                        .HasColumnType("varchar(255)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(6)");

                    b.HasKey("ScheduleID");

                    b.HasIndex("ServiceID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ServiceRequest", b =>
                {
                    b.Property<string>("ServiceRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("AddedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CartID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("providerServiceProviderID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("providerServiceServiceID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ServiceRequestID");

                    b.HasIndex("CartID");

                    b.HasIndex("providerServiceProviderID", "providerServiceServiceID");

                    b.ToTable("ServiceRequests");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.TimeSlot", b =>
                {
                    b.Property<string>("TimeSlotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("ProviderAvailabilityID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(6)");

                    b.Property<bool?>("enable")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("TimeSlotID");

                    b.HasIndex("ProviderAvailabilityID");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.UserReview", b =>
                {
                    b.Property<string>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("OrderID");

                    b.ToTable("UserReviews");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.Admin", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.Customer", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CartID")
                        .HasColumnType("varchar(255)");

                    b.HasIndex("CartID")
                        .IsUnique();

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.User");

                    b.Property<bool>("isVerified")
                        .HasColumnType("tinyint(1)");

                    b.ToTable("ServiceProviders", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Company", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.ServiceProviders.Provider");

                    b.Property<string>("license")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Worker", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.ServiceProviders.Provider");

                    b.Property<string>("CriminalRecord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable("Workers", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Consultant", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Entities.Users.ServiceProviders.Worker");

                    b.ToTable("Consultants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Service", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Criteria", "Criteria")
                        .WithMany("Services")
                        .HasForeignKey("CriteriaID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Service", "ParentService")
                        .WithMany("ChildServices")
                        .HasForeignKey("ParentServiceID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Criteria");

                    b.Navigation("ParentService");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Order", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderAvailability", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", "ServiceProvider")
                        .WithMany("Availabilities")
                        .HasForeignKey("ServiceProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceProvider");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderService", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", "Provider")
                        .WithMany("ProviderServices")
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Service", "Service")
                        .WithMany("ProviderServices")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Schedule", b =>
                {
                    b.HasOne("Service", "Service")
                        .WithMany("Schedules")
                        .HasForeignKey("ServiceID");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ServiceRequest", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Cart", "Cart")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.ProviderService", "providerService")
                        .WithMany("ServiceRequest")
                        .HasForeignKey("providerServiceProviderID", "providerServiceServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("providerService");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.TimeSlot", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.ProviderAvailability", "ProviderAvailability")
                        .WithMany("Slots")
                        .HasForeignKey("ProviderAvailabilityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProviderAvailability");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.UserReview", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.Admin", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.Customer", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Cart", "Cart")
                        .WithOne("Customer")
                        .HasForeignKey("WebApplication1.Models.Entities.Users.Customer", "CartID");

                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.Customer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Company", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.ServiceProviders.Company", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Worker", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.ServiceProviders.Worker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Consultant", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Users.ServiceProviders.Worker", null)
                        .WithOne()
                        .HasForeignKey("WebApplication1.Models.Entities.Users.ServiceProviders.Consultant", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Service", b =>
                {
                    b.Navigation("ChildServices");

                    b.Navigation("ProviderServices");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cart", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Criteria", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderAvailability", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.ProviderService", b =>
                {
                    b.Navigation("ServiceRequest");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Users.ServiceProviders.Provider", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("ProviderServices");
                });
#pragma warning restore 612, 618
        }
    }
}
