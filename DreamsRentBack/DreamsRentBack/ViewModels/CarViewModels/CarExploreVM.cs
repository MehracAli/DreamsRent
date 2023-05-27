using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.CarViewModels
{
    public class CarExploreVM
    {
        public int Id { get; set; }
        public List<Like> Likes { get; set; }
        public List<CarPhoto> CarPhotos { get; set; }
        public List<Rating> Ratings { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public double Price { get; set; }
        public double? Rating { get; set; }
        public Transmission Transmission { get; set; }
        public int Speed { get; set; }
        public FuelType FuelType { get; set; }
        public Engine Engine { get; set; }
        public int Year { get; set; }
        public int Capacity { get; set; }
        public Company Company { get; set; }
    }
}
