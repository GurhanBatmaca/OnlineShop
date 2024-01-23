using Data.Abstract;
using Entity;

namespace Data.Concrete.EfCore
{
    public class EfCoreOrderRepository: EfCoreGenericRepository<Order>,IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context):base(context)
        {            
        }

        private ShopContext? Context => _context as ShopContext;
    }
}