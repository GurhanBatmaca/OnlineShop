using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class FargotPasswordModel
{
    [Required(ErrorMessage ="E-Posta zorunludur!")]
    [EmailAddress(ErrorMessage ="Lütfen bir e-posta adresi yazınız.")]
    [Display(Name ="E-Posta")]
    public string? Email { get; set; }


}
