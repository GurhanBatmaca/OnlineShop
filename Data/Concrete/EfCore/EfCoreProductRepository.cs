using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;
using Shared.Models;

namespace Data.Concrete.EfCore
{
    public class EfCoreProductRepository: EfCoreGenericRepository<Product>,IProductRepository
    {
        public EfCoreProductRepository(ShopContext context):base(context)
        {           
        }

        protected ShopContext? Context => _context as ShopContext;

        public async Task<List<Product>> GetAllProductsAsync(int page, int pageSize)
        {
            return await Context!.Products.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAllProductsCountAsync()
        {
            return await Context!.Products.CountAsync();
        }

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

        public async Task<Product?> GetProductForUpdate(int id)
        {
            return await Context!.Products
                                .Include(e=>e.ProductCategories!)
                                .ThenInclude(e=>e.Category)
                                .FirstOrDefaultAsync(e=>e.Id == id);
        }

        public async Task<List<Product?>> GetProductListByIds(int[] ids)
        {
            var entitys = Context!.Products.Where(e=>e.IsApproved).AsQueryable();

            var products = Enumerable.Empty<Product?>().AsQueryable();

            foreach (var entity in entitys)
            {
                foreach (var id in ids)
                {
                    if(entity.Id == id )
                    {
                        products.Append(entity);
                    };
                }
            }

            return await products.ToListAsync();

            // var products = entitys.Select(e => ids.Any(i=>i == e.Id) ? e : null);
            // return await products.ToListAsync();
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

        public async Task UpdateProductAsync(ProductModel product, int[] categoriesIds)
        {
            var entity = await Context!.Products
                                        .Include(e=>e.ProductCategories!)
                                        .ThenInclude(e=>e.Category)
                                        .FirstOrDefaultAsync(e=>e.Id == product.Id);

            entity!.Id = product.Id;
            entity!.Name = product.Name;
            entity.Price = product.Price;
            entity.StockQuantity = product.StockQuantity;
            entity.Url = UrlGenerator.Create(product.Name!);
            entity.Description = product.Description;
            entity.IsApproved = product.IsApproved;
            entity.IsHome = product.IsHome;
            entity.ImageUrl = product.ImageUrl;
            entity.Weight = product.Weight;

            entity.ProductCategories = categoriesIds.Select(catId=> new ProductCategory{
                ProductId = entity.Id,
                CategoryId = catId
            }).ToList();

            await Context.SaveChangesAsync();                            
        }

    }
    
}