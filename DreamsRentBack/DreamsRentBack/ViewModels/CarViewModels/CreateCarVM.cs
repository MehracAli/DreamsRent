using DreamsRentBack.Entities.CarModels;
using Microsoft.Build.Framework;

namespace DreamsRentBack.ViewModels.CarViewModels
{
    public class CreateCarVM
    {
        public int Id { get; set; }
        public List<int> CarPhotosIds { get; set; }
        public List<IFormFile> iff_CarPhotos { get; set; }
        public int ModelId { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }

        //Extra Services Start
        public List<int> ServicesIds { get; set; }
        //Extra Services End

        //Specifications Start
        public int BodyId { get; set; }
        public int BrandId { get; set; }
        public int TransmissionId { get; set; }
        public int FuelTypeId { get; set; }
        public byte Speed { get; set; }
        public int DrivertrianId { get; set; }
        public int Year { get; set; }
        public int AirConditionId { get; set; }
        public string VIN { get; set; }
        public int Door { get; set; }
        public int BrakeId { get; set; }
        public int EngineId { get; set; }
        //Specifications End

        //Features Start
        public List<int> FeaturesIds { get; set; }
        //Features End

        public CreateCarVM()
        {
            CarPhotosIds = new();
            iff_CarPhotos = new();
            ServicesIds = new();
            FeaturesIds = new();
        }
    }
}
