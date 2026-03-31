using System.ComponentModel.DataAnnotations;

namespace YatriiWorld.MVC.ViewModels.LoginRegister
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email Address or Username is required.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
