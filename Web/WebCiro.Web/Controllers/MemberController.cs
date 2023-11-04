using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCiro.Core.Entities.Authentication;

namespace WebCiro.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()

        {
            return View();
        }

        public IActionResult Logout()

        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
