using System.ComponentModel.DataAnnotations;

namespace WebCiro.Models.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
        }

        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "Email Alanı Boş Olamaz")]
        [Display(Name = "Email Adresiniz")]
        [EmailAddress(ErrorMessage = "Email Formatı Yanliş")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Boş Olamaz")]
        [Display(Name = "Şifre Alanı Boş Olamaz")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
