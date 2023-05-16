using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace DreamsRentBack.ViewModels.Identify
{
    public class CompanyRegisterVM
    {
        [System.ComponentModel.DataAnnotations.Required, StringLength(maximumLength:30)]
        public string CompanyName { get; set; }

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

        [System.ComponentModel.DataAnnotations.Required]
        public bool Terms { get; set; }
    }
}
