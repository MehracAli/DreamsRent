using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.CarModels
{
    public class Body:BaseEntity
    {
        public string Name { get; set; }
        public string BodyPhoto { get; set; }
        [NotMapped]
        public IFormFile iff_BodyPhoto { get; set; }
        public List<Car> Cars { get; set; }

        public Body()
        {
            Cars = new();
        }
    }
}
