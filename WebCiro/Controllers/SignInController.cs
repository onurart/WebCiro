using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebCiro.Extenisons;
using WebCiro.Models.Authentication;
using WebCiro.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebCiro.Controllers
{
    public class SignInController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public SignInController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager = null)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Dashboard");
            var hasUser = await _userManager.FindByEmailAsync(model.Email);


            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email Yanliş");
            }
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);

            }
            ModelState.AddModelErrorList(new List<string>() { "Email veya şifre yanliş" });
            return View();
        }
    }
}
