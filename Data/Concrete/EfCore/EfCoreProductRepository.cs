using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class EfCoreProductRepository: EfCoreGenericRepository<Product>,IProductRepository
    {
        public EfCoreProductRepository(ShopContext context):base(context)
        {           
        }

        protected ShopContext? Context => _context as ShopContext;

        public async Task<List<Product>> GetHomePageProducts(int page,int pageSize)
        {
            var products = Context!.Products
                                    .Where(i=>i.IsHome && i.IsApproved)
                                    .AsQueryable();

            return await products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetHomePageProductsCount()
        {
            return await Context!.Products.Where(i=>i.IsHome && i.IsApproved).CountAsync();
        }
    }
    
}