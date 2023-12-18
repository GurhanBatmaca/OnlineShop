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

        public async Task<Product?> GetProductDetails(string url)
        {
            return await Context!.Products
                                        .Where(e=>e.Url == url && e.IsApproved)
                                        .Include(e=>e.ProductCategories!)
                                        .ThenInclude(e=>e.Category)
                                        .FirstOrDefaultAsync();
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

        public async Task<List<Product>> GetSearchResult(string query, int page, int pageSize)
        {
            var products = Context!.Products.Where(e=>e.IsApproved).AsQueryable();

            if(!string.IsNullOrEmpty(query))
            {
                products = products.Include(e=>e.ProductCategories!)
                                    .ThenInclude(e=>e.Category)              
                                    .Where(e=>e.Name!.ToLower()!.Contains(query.ToLower()) || e.Description!.ToLower().Contains(query.ToLower()) || e.ProductCategories!.Any(e=>e.Category!.Name!.ToLower()!.Contains(query.ToLower())));
            }

            return await products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetSearchResultCount(string query)
        {
            var products = Context!.Products.Where(e=>e.IsApproved).AsQueryable();

            if(!string.IsNullOrEmpty(query))
            {
                products = products.Include(e=>e.ProductCategories!)
                                    .ThenInclude(e=>e.Category)              
                                    .Where(e=>e.Name!.ToLower()!.Contains(query.ToLower()) || e.Description!.ToLower().Contains(query.ToLower()) || e.ProductCategories!.Any(e=>e.Category!.Name!.ToLower()!.Contains(query.ToLower())));
            }

            return await products.CountAsync();
        }
    }
    
}