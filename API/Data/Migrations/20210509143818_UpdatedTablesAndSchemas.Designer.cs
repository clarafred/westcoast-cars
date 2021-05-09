﻿// <auto-generated />
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210509143818_UpdatedTablesAndSchemas")]
    partial class UpdatedTablesAndSchemas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Country")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Email")
                        .HasColumnType("VARCHAR(128)");

                    b.Property<string>("FirstName")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("LastName")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<string>("Phone")
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("UserName")
                        .HasColumnType("VARCHAR(128)");

                    b.HasKey("Id");

                    b.ToTable("User", "Customers");
                });

            modelBuilder.Entity("API.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(80)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturer", "Vehicles");
                });

            modelBuilder.Entity("API.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("FuelType")
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("GearType")
                        .HasColumnType("VARCHAR(40)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<short>("ModelYear")
                        .HasColumnType("SMALLINT");

                    b.Property<string>("RegNum")
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicle", "Vehicles");
                });

            modelBuilder.Entity("API.Entities.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(80)");

                    b.HasKey("Id");

                    b.ToTable("VehicleModel", "Vehicles");
                });

            modelBuilder.Entity("API.Entities.Vehicle", b =>
                {
                    b.HasOne("API.Entities.Brand", "Brand")
                        .WithMany("Vehicles")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.VehicleModel", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("API.Entities.Brand", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("API.Entities.VehicleModel", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
