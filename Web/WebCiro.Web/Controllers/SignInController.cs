using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCiro.Core.Entities.Authentication;
using WebCiro.Core.Entities.ViewModels;

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
                ModelState.AddModelError(string.Empty, "Email Yanlış");
                return View();
            }

                var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);
                if (signInResult.Succeeded)
                {
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
                return View();
            //if (hasUser.IPAddress == GetUserIPAddress() && hasUser.MaccAdress == GetUserMachineID())
            //{
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Geçersiz IP Adresi veya MachineID");
            //    return View();
            //}
        }
        private string GetUserIPAddress()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString();
        }
        private string GetUserMachineID()
        {
            return Environment.MachineName.GetHashCode().ToString();
        }

    }
}
