using System.ComponentModel.DataAnnotations;

namespace MVC.Models.ViewModels
{
    public class AccountRegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifreler birleriyle uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
