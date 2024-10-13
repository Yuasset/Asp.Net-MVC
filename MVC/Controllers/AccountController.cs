using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Identity;
using MVC.Models.ViewModels;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppUserRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppUserRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM accountLoginVM)
        {
            var username = await userManager.FindByEmailAsync(accountLoginVM.Email);
            if (ModelState.IsValid && username != null)
            {
                var result = await signInManager.PasswordSignInAsync(username, accountLoginVM.Password, accountLoginVM.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş başarısız");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM accountRegisterVM)
        {
            if (ModelState.IsValid) //Validation işlemi
            {
                AppUser appUser = new AppUser //ViewModel'den AppUser'a dönüştürme
                {
                    Name = accountRegisterVM.Name,
                    Surname = accountRegisterVM.Surname,
                    UserName = accountRegisterVM.Username,
                    Email = accountRegisterVM.Email,
                    PhoneNumber = accountRegisterVM.PhoneNumber
                };
                

                //ASync Register
                var result = await userManager.CreateAsync(appUser, accountRegisterVM.ConfirmPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else //Hata varsa result.Errors'da hatalar listelenir.
                {
                    foreach (var item in result.Errors) //Hatalar result.Errors içinden alınarak listelenir
                    {
                        ModelState.AddModelError("", item.Description); //Hata mesajları ModelState'e eklenir. .AddModelError(KEY, DESCRIPTION); medotu kullanarak gerçekleştirilir. ValidationSummary'de bu hatalar listelenir.
                    }
                }
            }
            else
            {
                return View(accountRegisterVM); //Hata varsa ViewModel'i geri döndür. Önceden girilen bilgiler kaybolmadan inputlar dolu gelmesi amaçlanır.
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
