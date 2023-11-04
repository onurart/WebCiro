using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.Entities.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }

        public SignUpViewModel(string userName, string email, string phone, string password, string maccAdress, string ipAdress)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        [Required(ErrorMessage = "Kullanıcı Ad Alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adınız")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Adresi Alanı boş bırakılamaz")]
        [Display(Name = "Email Adresiniz")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Alanı boş bırakılamaz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrar Alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirm { get; set; }

        public string IPAddress { get; set; }
        public string MaccAdress { get; set; }
    }
}
