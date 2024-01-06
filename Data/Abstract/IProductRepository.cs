using Entity;
using Shared.Models;

namespace Data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>> GetHomePageProducts(int page,int pageSize);
        Task<int> GetHomePageProductsCount();
        Task<List<Product>> GetProdutsByCategory(string category,int page,int pageSize);
        Task<int> GetProdutsCountByCategory(string category);
        Task<Product?> GetProductDetails(string url);
        Task<List<Product>> GetSearchResult(string query,int page,int pageSize);
        Task<int> GetSearchResultCount(string query);
        Task<List<Product>> GetAllProductsAsync(int page,int pageSize);
        Task<int> GetAllProductsCountAsync();
        Task<Product?> GetProductForUpdate(int id);
        Task UpdateProductAsync(ProductModel product,int[] categoriesIds);
    }
}