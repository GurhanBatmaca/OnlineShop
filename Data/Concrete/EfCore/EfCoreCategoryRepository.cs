using Data.Abstract;
using Entity;

namespace Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository: EfCoreGenericRepository<Category>,ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context):base(context)
        {           
        }

        protected ShopContext? Context => _context as ShopContext;
    }
    
}