using System.ComponentModel.DataAnnotations;
using Shared.ViewModels;

namespace Shared.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Ad zorunludur")]
        [Display(Name ="Ad")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [Display(Name ="Soyad")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Adres zorunludur")]
        [Display(Name ="Adres")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Şehir zorunludur")]
        [Display(Name ="Şehir")]
        public string? City { get; set; }
        
        [Required(ErrorMessage = "Telefon zorunludur")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Telefon")]
        public int Phone { get; set; }

        [Display(Name ="Not")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "E-Posta zorunludur")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="E-Posta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kart üzerindeki Ad zorunludur")]
        [Display(Name ="Ad-Soyad")]
        public string? CardName { get; set; }

        [Required(ErrorMessage = "Kredi kartı numarası zorunludur")]
        [Display(Name ="Kredi kartı numarası")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Kredi kartı son kullanma ayı zorunludur")]
        [Display(Name ="Son kullanma ayı")]
        public string? ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Kredi kartı son kullanma yılı zorunludur")]
        [Display(Name ="Son kullanma yılı")]
        public string? ExpirationYear { get; set; }

        [Required(ErrorMessage = "Güvenlik numarası zorunludur")]
        [Display(Name ="Güvenlik numarası")]
        public string? Cvc { get; set; }
        public CartViewModel? CartModel { get; set; }


        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
    }
}