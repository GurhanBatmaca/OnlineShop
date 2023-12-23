using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class ResetPasswordModel
{
    [Required(ErrorMessage ="Şifre zorunludur!")]
    [DataType(DataType.Password,ErrorMessage ="Girdiğiniz şifre, şifre formatlarına uygun değil.")]
    [StringLength(20,MinimumLength = 6,ErrorMessage = "Şifre 6-20 karakter uzunluğunda olmalı.")]
    [Display(Name ="Şifre")]
    public string? Password { get; set; }

    [Required(ErrorMessage ="Şifre tekrarı zorunludur!")]
    [DataType(DataType.Password,ErrorMessage ="Girdiğiniz şifre, şifre formatlarına uygun değil.")]
    [Display(Name ="Şifre tekrarı")]
    [Compare("Password",ErrorMessage ="Şifre ve şifre tekrarı aynı olmalı.")]
    public string? RePassword { get; set; }
    public string? Token { get; set; }
    public string? UserId { get; set; }
}
