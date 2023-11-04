using Microsoft.AspNetCore.Mvc;


namespace WebCiro.Web.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
