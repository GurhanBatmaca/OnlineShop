using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Data.Concrete.EfCore
{
    public class EfCoreOrderStateRepository: EfCoreGenericRepository<OrderState>,IOrderStateRepository
    {
        public EfCoreOrderStateRepository(ShopContext context):base(context)
        {
            
        }
        protected ShopContext? Context => _context as ShopContext;

    }
}