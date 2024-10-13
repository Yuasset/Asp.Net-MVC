using MVC.Models.Identity;

namespace MVC.Models.ViewModels
{
    public class UserRoleVM
    {
        public AppUser User { get; set; }
        public List<AppUserRole> UserRoles { get; set; }
    }
}
