using DreamsRentBack.Entities;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DreamsRentBack.DAL
{
    public class DreamsRentDbContext : IdentityDbContext<User>
    {
        public DreamsRentDbContext(DbContextOptions<DreamsRentDbContext> options) : base(options)
        {

        }

        //Client Models Start
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyPickupLocation> CompanyPickupLocations { get; set; }
        public DbSet<CompanyDropoffLocation> CompanyDropoffLocations { get; set; }
        public DbSet<DropoffLocation> DropoffLocations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PayCard> PayCards { get; set; }
        public DbSet<PayCardType> PayCardTypes { get; set; }
        public DbSet<PickupLocation> PickupLocations { get; set; }
        public DbSet<Street> Streets { get; set; }
        //Client Models End

        //Car Models Start
        public DbSet<AirCondition> AirConditions { get; set; }
        public DbSet<Body> Bodys { get; set; }
        public DbSet<Brake> Brakes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarFeatures> CarsFeatures { get; set; }
        public DbSet<CarPhoto> CarPhotos { get; set; }
        public DbSet<Drivetrian> Drivetrians { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        //Car Models End
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}
