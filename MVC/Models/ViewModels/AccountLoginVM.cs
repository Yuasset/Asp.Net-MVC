using System.ComponentModel.DataAnnotations;

namespace MVC.Models.ViewModels
{
    public class AccountLoginVM
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
