using Microsoft.AspNetCore.Mvc;

namespace YatriiWorld.MVC.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
