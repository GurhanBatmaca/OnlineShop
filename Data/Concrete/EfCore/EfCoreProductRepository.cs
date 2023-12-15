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

        public async Task<List<Product>> GetProdutsByCategory(string category, int page, int pageSize)
        {
            var products = Context!.Products.Where(i=>i.IsApproved).AsQueryable();

            if(!string.IsNullOrEmpty(category))
            {
                products = products.Include(e=>e.ProductCategories!)
                                    .ThenInclude(e=>e.Category)
                                    .Where(e=>e.ProductCategories!.Any(i=>i.Category!.Url == category));
            }

            return await products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
                                   
        }

        public async Task<int> GetProdutsCountByCategory(string category)
        {
            var products = Context!.Products.Where(i=>i.IsApproved).AsQueryable();

            if(!string.IsNullOrEmpty(category))
            {
                products = products.Include(e=>e.ProductCategories!)
                                    .ThenInclude(e=>e.Category)
                                    .Where(e=>e.ProductCategories!.Any(i=>i.Category!.Url == category));
            }

            return await products.CountAsync();
        }

    }
    
}