using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.ClientModels
{
    public class PayCard:BaseEntity
    {
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        public string Date { get; set; }
        public string HolderName { get; set; }
        public string HolderSurname { get; set; }
        public double Balance { get; set; } = 1000;
        public string cvv { get; set; }
        public int PayCardTypeId { get; set; }
        public PayCardType PayCardType { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
