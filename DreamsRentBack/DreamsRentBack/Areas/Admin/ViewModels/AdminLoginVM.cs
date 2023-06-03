using System.ComponentModel.DataAnnotations;

namespace DreamsRentBack.Areas.Admin.ViewModels
{
    public class AdminLoginVM
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
