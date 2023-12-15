using Entity;

namespace Data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>> GetHomePageProducts(int page,int pageSize);
        Task<int> GetHomePageProductsCount();
    }
}