using Data.Abstract;
using Entity;

namespace Data.Concrete.EfCore
{
    public class EfCoreCartRepository: EfCoreGenericRepository<Cart>,ICartRepository
    {
        public EfCoreCartRepository(ShopContext context):base(context)
        {
            
        }
        protected ShopContext? Context => _context as ShopContext;
    }
}