using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IProductService
    {
        string? Message { get; set; }

        Task<ProductListViewModel> GetAllProductsAsync(int page);
        Task<ProductListViewModel> GetHomePageProducts(int page);
        Task<ProductListViewModel> GetProductsByCategory(string category,int page);
        Task<ProductDetailsModel?> GetProductDetails(string url);
        Task<ProductListViewModel> GetSearchResult(string query, int page);
        Task<bool> CreateAsync(ProductModel model,int[] categoriesIds);
        Task<bool> DeleteProduct(int id);
        Task<ProductModel?> GetProductForUpdate(int id);
        Task<bool> UpdateProductAsync(ProductModel model,int[] categoriesIds);
        Task<ProductViewModel?> GetProductById(int id);
        Task<ProductListViewModel> GetProductListByIds(IEnumerable<int> ids);
    }
}