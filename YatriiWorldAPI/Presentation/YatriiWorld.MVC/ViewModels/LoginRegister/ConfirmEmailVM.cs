using System.ComponentModel.DataAnnotations;

namespace YatriiWorld.MVC.ViewModels.LoginRegister
{
    public class ConfirmEmailVM
    {
        public string Email { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Code { get; set; }
    }
}
