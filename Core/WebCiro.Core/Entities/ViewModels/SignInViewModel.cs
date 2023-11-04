using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.Entities.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
        }

        public SignInViewModel(string email, string password, string ipAddress, string machineID)
        {
            Email = email;
            Password = password;
            IPAddress = ipAddress;  // Doğru şekilde atanmış
            MaccAdress = machineID;  // Doğru şekilde atanmış
        }

        [Required(ErrorMessage = "Email Alanı Boş Olamaz")]
        [Display(Name = "Email Adresiniz")]
        [EmailAddress(ErrorMessage = "Email Formatı Yanliş")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Boş Olamaz")]
        [Display(Name = "Şifre Alanı Boş Olamaz")]
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string MaccAdress { get; set; }
        public bool RememberMe { get; set; }
    }
}
