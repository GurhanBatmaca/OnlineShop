using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class RegisterModel
{
    [Required(ErrorMessage ="Ad zorunludur!")]
    [Display(Name ="Ad")]
    [StringLength(30,MinimumLength = 3,ErrorMessage = "Ad 3-20 karakter uzunluğunda olmalı.")]   
    public string? FirsName { get; set; }

    [Required(ErrorMessage ="Soyad zorunludur!")]
    [Display(Name ="Soyad")]
    [StringLength(30,MinimumLength = 3,ErrorMessage = "Soyad 3-20 karakter uzunluğunda olmalı.")]   
    public string? LastName { get; set; }

    [Required(ErrorMessage ="Kullanıcı adı zorunludur!")]
    [Display(Name ="Kullanıcı adı")]
    [StringLength(20,MinimumLength = 3,ErrorMessage = "Kullanıcı adı 3-20 karakter uzunluğunda olmalı.")]   
    public string? UserName { get; set; }

    [Required(ErrorMessage ="E-Posta zorunludur!")]
    [EmailAddress(ErrorMessage ="Lütfen bir e-posta adresi yazınız.")]
    [Display(Name ="E-Posta")]
    public string? Email { get; set; }

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

}
