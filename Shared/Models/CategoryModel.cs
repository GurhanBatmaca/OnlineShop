using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Kategori adı zorunlur")]
        [Display(Name = "Kategori adı")]
        [StringLength(40,MinimumLength =3)]
        public string? Name { get; set; }
        public string? Url { get; set; }


    }
}