using Data.Abstract;
using Entity;

namespace Data.Concrete.EfCore
{
    public class EfCoreProductRepository: EfCoreGenericRepository<Product>,IProductRepository
    {
        public EfCoreProductRepository(ShopContext context):base(context)
        {           
        }

        protected ShopContext? Context => _context as ShopContext;
    }
    
}