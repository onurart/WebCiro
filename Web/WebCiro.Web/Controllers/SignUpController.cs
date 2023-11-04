using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebCiro.Core.Entities.Authentication;
using WebCiro.Core.Entities.ViewModels;

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
            if (!ModelState.IsValid)
            {
                // ModelState geçerli değilse, uygun bir işlem yapabilirsiniz.
                // Örneğin, hata mesajlarını loglama veya kullanıcıya bildirme gibi.
            }

            // Benzersiz kimlik tanımlayıcısı oluşturma (örneğin, GUID kullanılabilir)
            string userIdentity = Guid.NewGuid().ToString();

            var user = new AppUser
            {
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Email = request.Email,
                UserIdentity = userIdentity  // Kullanıcının profiline kimlik tanımlayıcısını ekleme
            };

            var identityResult = await _userManager.CreateAsync(user, request.PasswordConfirm);

            if (identityResult.Succeeded)
            {
                // Başarı durumunda, kullanıcının tarayıcısına kimlik tanımlayıcısını içeren bir çerez ekleyebilirsiniz.
                // Bu çerez, kullanıcının sonraki taleplerinde kimliğini belirlemenize yardımcı olacaktır.

                // Örnek çerez oluşturma (bu sadece bir örnektir, gerçek bir uygulamada daha karmaşık olabilir)
                var userCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("UserIdentity", userIdentity, userCookieOptions);

                TempData["SuccessMessage"] = "Üyelik İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction(nameof(SignUpController.Index));
            }

            // Hata durumunda ModelState'a hata mesajlarını ekle
            //ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
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
