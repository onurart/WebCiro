using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCiro.Extenisons;
using WebCiro.Models.Authentication;
using WebCiro.Models.ViewModels;

namespace WebCiro.Controllers
{
    public class SignUpController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public SignUpController(UserManager<AppUser> userManager) { _userManager = userManager; }

        public IActionResult Index() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Index(SignUpViewModel request)
        {
            if (!ModelState.IsValid) { }
            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email }, request.PasswordConfirm);
            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik İşlemi  Başarıla Gerçekleşti";
                return RedirectToAction(nameof(SignUpController.Index));
            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }


    }
}
