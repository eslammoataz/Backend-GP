﻿// < auto - generated />
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using WebApplication1.Data;

//#nullable disable

//namespace WebApplication1.Migrations
//{
//    [DbContext(typeof(AppDbContext))]
//    partial class AppDbContextModelSnapshot : ModelSnapshot
//    {
//        protected override void BuildModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "7.0.14")
//                .HasAnnotation("Relational:MaxIdentifierLength", 64);

//            modelBuilder.Entity("WebApplication1.Data.Customer", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("longtext");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("longtext");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("longtext");

//                    b.Property<string>("email")
//                        .IsRequired()
//                        .HasColumnType("longtext");

//                    b.HasKey("Id");

//                    b.ToTable("Customers", (string)null);
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
