using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.Account
{
    public class ConsumerAccountVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserPhoto { get; set; }
        public PayCard PayCard { get; set; }

    }
}
