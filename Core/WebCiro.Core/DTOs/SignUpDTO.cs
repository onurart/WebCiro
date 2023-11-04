using System.ComponentModel.DataAnnotations;

namespace WebCiro.Core.DTOs
{
    public class SignUpDTO
    {
        public SignUpDTO()
        {

        }
        public SignUpDTO(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }
        [Required(ErrorMessage = "Kullanı Ad Alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adınız")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Adresi Alanı boş bırakılamaz")]
        [Display(Name = "EmailAdresiniz")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Alanı boş bırakılamaz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar Alanı boş bırakılamaz")]

        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirm { get; set; }
    }
}
