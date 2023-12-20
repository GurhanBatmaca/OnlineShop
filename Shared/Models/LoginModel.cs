using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class LoginModel
{
    [Required(ErrorMessage ="E-Posta zorunludur!")]
    [EmailAddress(ErrorMessage ="Lütfen bir e-posta adresi yazınız.")]
    [Display(Name ="E-Posta")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Şifre zorunludur!")]
    [DataType(DataType.Password,ErrorMessage ="Girdiğiniz şifre, şifre formatlarına uygun değil.")]
    [Display(Name ="Şifre")]
    public string? Password { get; set; }

    [Display(Name ="Beni hatırla")]
    public bool RememberMe { get; set; }
}
