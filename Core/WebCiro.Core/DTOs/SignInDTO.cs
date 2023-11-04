using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.DTOs
{
    public class SignInDTO
    {
        public SignInDTO()
        {
        }

        public SignInDTO(string email, string password)
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
