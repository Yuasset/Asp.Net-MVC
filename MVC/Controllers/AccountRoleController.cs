using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Identity;
using MVC.Models.ViewModels;

namespace MVC.Controllers
{
    public class AccountRoleController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppUserRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountRoleController(UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AppUserRole appUserRole)
        {
            var result = await roleManager.CreateAsync(appUserRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> UserRole()
        {
            var users = userManager.Users.ToList();
            var userRoleVM = new List<UserRoleVM>();

            //var userRole = await userManager.GetRolesAsync(users[0]);
            foreach (var user in users)
            {
                var role = await userManager.GetRolesAsync(user);
                var userRoles = role.Select(role => new AppUserRole
                {
                    Name = role
                }).ToList();
                userRoleVM.Add(new UserRoleVM
                {
                    User = user,
                    UserRoles = userRoles
                });
            }

            return View(userRoleVM);
        }
        public IActionResult UserRoleAdd()
        {
            return View();
        }
        public async Task<IActionResult> UserRoleUpdate(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            var roles = roleManager.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(user);
            var roleAssignVM = new List<RoleAssignVM>(); //Rolleri taşıyacak ve aktif ve pasif olarak rolleri işaretini tutan taşıyıcı sınıf List<> olarak oluşturdum.
            foreach (var role in roles)
            {
                roleAssignVM.Add(new RoleAssignVM
                {
                    ID = role.Id,
                    Name = role.Name,
                    HasAssign = userRoles.Contains(role.Name) //Tüm rollerlerin isimlerini alıp userRoles içinde tanımlı olan rolleri true olmayanları false yap.
                });
            }
            return View(roleAssignVM);
        }
        [HttpPost]
        public async Task<IActionResult> UserRoleUpdate(int id, List<RoleAssignVM> roleAssignVM)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            foreach (var item in roleAssignVM)
            {
                if (item.HasAssign == true)
                {
                    await userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }
            await signInManager.RefreshSignInAsync(user); //Kullanıcı rolleri güncelleme sonrası güncel haliyle devam edebilemesi için kullanıcı oturumunu otomatik yenileme işlemini yapar.

            return RedirectToAction("UserRole");
        }
    }
}
