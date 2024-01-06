using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.ViewModels;

namespace Shared.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Ürün adı zorunlur")]
        [Display(Name = "Ürün adı")]
        [StringLength(40,MinimumLength =3)]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Fiyat zorunludur")]
        [Display(Name = "Fiyat")]
        [Range(1,99999,ErrorMessage = "Fiyat 1-99999 arsında olmalıdır")]
        public double Price { get; set; } 

        [Required(ErrorMessage ="Açıklama zorunlur")]
        [Display(Name = "Açıklama")]
        [StringLength(200,MinimumLength =20,ErrorMessage ="Açıklama 20-200 karakter uzunluğunda olmalıdır.")]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Ağırlık zorunlur")]
        [Display(Name = "Ağırlık (gr)")]
        [Range(0.1,99999,ErrorMessage = "Ağırlık 0.1-99999 arsında olmalıdır")]
        public double Weight { get; set; }

        [Display(Name = "Onaylı")]
        public bool IsApproved { get; set; }

        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }

        [Required(ErrorMessage ="Stok zorunlur")]
        [Display(Name = "Stok")]
        [Range(1,99999,ErrorMessage = "Stok 1-99999 arsında olmalıdır")]
        public int StockQuantity { get; set; }

        [Display(Name = "Resim")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public List<CategoryViewModel>? SelectedCategories { get; set; }


    }
}