using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Areas.Admin.Controllers
{
    [Area("Admim")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
