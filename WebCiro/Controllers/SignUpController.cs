using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        // Method to generate a unique MachineID (this is a simple example)
        private string GenerateMachineId()
        {
            // You might want to include more information to make it more unique
            return Environment.MachineName.GetHashCode().ToString();
        }
        private string GetPublicIPAddress()
        {
            using (var client = new WebClient())
            {
                string ipAddress = client.DownloadString("https://api64.ipify.org?format=text");
                return ipAddress;
            }
        }



    }
}
