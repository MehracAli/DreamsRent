using System.ComponentModel.DataAnnotations;

namespace DreamsRentBack.ViewModels.Account
{
    public class ResetPasswordVM
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
