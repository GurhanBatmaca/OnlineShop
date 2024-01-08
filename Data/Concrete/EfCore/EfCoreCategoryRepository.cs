using Data.Abstract;
using Entity;
using Shared.Helpers;
using Shared.Models;

namespace Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository: EfCoreGenericRepository<Category>,ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context):base(context)
        {           
        }

        protected ShopContext? Context => _context as ShopContext;

        public async Task UpdateCategoryAsync(CategoryModel model)
        {
            var entity = Context!.Categories.Find(model.Id);
            entity!.Name = model.Name;
            entity.Url = UrlGenerator.Create(model.Name!);
            await Context.SaveChangesAsync();
        }

    }
    
}