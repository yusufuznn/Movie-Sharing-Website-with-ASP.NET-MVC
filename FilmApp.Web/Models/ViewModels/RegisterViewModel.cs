using System.ComponentModel.DataAnnotations;

namespace FilmApp.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

    }
}
