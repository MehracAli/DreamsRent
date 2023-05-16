﻿// <auto-generated />
using System;
using DreamsRentBack.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DreamsRentBack.Migrations
{
    [DbContext(typeof(DreamsRentDbContext))]
    [Migration("20230514125653_createEntitiesTables")]
    partial class createEntitiesTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.AirCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AirConditions");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Body", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bodys");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Brake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brakes");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AirConditionId")
                        .HasColumnType("int");

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("BodyId")
                        .HasColumnType("int");

                    b.Property<int>("BrakeId")
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("BreakId")
                        .HasColumnType("int");

                    b.Property<byte>("Capacity")
                        .HasColumnType("tinyint");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Doors")
                        .HasColumnType("tinyint");

                    b.Property<int>("DrivertrianId")
                        .HasColumnType("int");

                    b.Property<int>("DrivetrianId")
                        .HasColumnType("int");

                    b.Property<int>("EngineId")
                        .HasColumnType("int");

                    b.Property<int>("FuelTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickupDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Speed")
                        .HasColumnType("tinyint");

                    b.Property<int>("TransmissionId")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.Property<byte>("Year")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AirConditionId");

                    b.HasIndex("BodyId");

                    b.HasIndex("BrakeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DrivetrianId");

                    b.HasIndex("EngineId");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("TransmissionId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarsFeatures");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarFeaturesAndCars", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarFeaturesId")
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarFeaturesId");

                    b.HasIndex("CarId");

                    b.ToTable("CarFeaturesAndCars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("CarPhotos");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Drivetrian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivetrians");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Engine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("HorsePower")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.ExtraService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ExtraServices");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.ExtraServicesAndCars", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("ExtraServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ExtraServiceId");

                    b.ToTable("ExtraServicesAndCars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Transmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId1");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Verification")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DreamsRentBack.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Car", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.CarModels.AirCondition", "AirCondition")
                        .WithMany("Cars")
                        .HasForeignKey("AirConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Body", "Body")
                        .WithMany("Cars")
                        .HasForeignKey("BodyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Brake", "Brake")
                        .WithMany("Cars")
                        .HasForeignKey("BrakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.ClientModels.Company", "Company")
                        .WithMany("Cars")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Drivetrian", "Drivetrian")
                        .WithMany("Cars")
                        .HasForeignKey("DrivetrianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Engine", "Engine")
                        .WithMany("Cars")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.FuelType", "FuelType")
                        .WithMany("Cars")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Transmission", "Transmission")
                        .WithMany("Cars")
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirCondition");

                    b.Navigation("Body");

                    b.Navigation("Brake");

                    b.Navigation("Brand");

                    b.Navigation("Company");

                    b.Navigation("Drivetrian");

                    b.Navigation("Engine");

                    b.Navigation("FuelType");

                    b.Navigation("Transmission");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarFeaturesAndCars", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.CarModels.CarFeatures", "CarFeatures")
                        .WithMany("FeaturesAndCars")
                        .HasForeignKey("CarFeaturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.Car", "Car")
                        .WithMany("FeaturesAndCars")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("CarFeatures");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarPhoto", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.CarModels.Car", "Car")
                        .WithMany("CarPhotos")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.ExtraServicesAndCars", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.CarModels.Car", "Car")
                        .WithMany("ServicesAndCars")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.CarModels.ExtraService", "ExtraService")
                        .WithMany("ServicesAndCars")
                        .HasForeignKey("ExtraServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("ExtraService");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Comment", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.CarModels.Car", "Car")
                        .WithMany("Comments")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId1");

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Company", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.ClientModels.Location", "Location")
                        .WithMany("Companies")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("DreamsRentBack.Entities.ClientModels.Company", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Location", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.ClientModels.City", "City")
                        .WithMany("Locations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Street", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.ClientModels.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
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
                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", null)
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

                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DreamsRentBack.Entities.ClientModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.AirCondition", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Body", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Brake", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Brand", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Car", b =>
                {
                    b.Navigation("CarPhotos");

                    b.Navigation("Comments");

                    b.Navigation("FeaturesAndCars");

                    b.Navigation("ServicesAndCars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.CarFeatures", b =>
                {
                    b.Navigation("FeaturesAndCars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Drivetrian", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Engine", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.ExtraService", b =>
                {
                    b.Navigation("ServicesAndCars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.FuelType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.CarModels.Transmission", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.City", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Streets");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Company", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.Location", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("DreamsRentBack.Entities.ClientModels.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
