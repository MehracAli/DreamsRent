using DreamsRentBack.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DreamsRentBack.ViewModels.Identify
{
    public class ConsumerRegisterVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [StringLength(maximumLength: 10)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        
        [Required]
        public bool Terms { get; set; }
    }
}
