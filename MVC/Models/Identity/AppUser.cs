using Microsoft.AspNetCore.Identity;
using MVC.Models.Market;

namespace MVC.Models.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
