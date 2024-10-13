using Microsoft.AspNetCore.Identity;

namespace MVC.Models.Identity
{
    public class AppUserRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
