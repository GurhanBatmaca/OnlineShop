using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<ProductListViewModel> GetHomePageProducts(int page);
        Task<ProductListViewModel> GetProductsByCategory(string category,int page);
        Task<ProductDetailsModel?> GetProductDetails(string url);
    }
}