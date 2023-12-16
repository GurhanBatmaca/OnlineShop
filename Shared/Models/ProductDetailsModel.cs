using Shared.ViewModels;

namespace Shared.Models
{
    public class ProductDetailsModel
    {
        public ProductViewModel? Product { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
    }
}