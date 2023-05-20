using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.Account
{
    public class CompanyVM
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoto { get; set; }
        public User User { get; set; }
        public Location Location { get; set; }
        public List<Car> Cars { get; set; }

        public CompanyVM()
        {
            Cars = new();
        }
    }
}
