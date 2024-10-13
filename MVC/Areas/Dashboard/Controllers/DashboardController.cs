using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")] //Admin Rolüne sahip olanlar bu controller'a erişebilir.
    [Area("Dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
