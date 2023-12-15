using Shared.ViewModels;

namespace Shared.Models
{
    public class ProductListViewModel
    {       
        public List<ProductViewModel>? Products { get; set; }
        public PageInfoModel? PageInfo { get; set; }
    }
}