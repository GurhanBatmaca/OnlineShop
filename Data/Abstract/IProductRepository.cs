using Entity;

namespace Data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>> GetHomePageProducts(int page,int pageSize);
        Task<int> GetHomePageProductsCount();
        Task<List<Product>> GetProdutsByCategory(string category,int page,int pageSize);
        Task<int> GetProdutsCountByCategory(string category);
        Task<Product?> GetProductDetails(string url);
    }
}