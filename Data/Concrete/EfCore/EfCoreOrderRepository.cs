using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class EfCoreOrderRepository: EfCoreGenericRepository<Order>,IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context):base(context)
        {            
        }

        private ShopContext? Context => _context as ShopContext;

        public async Task<List<Order>?> GetOrders(string userId, int page, int pageSize)
        {
            var orders = Context!.Orders
                                .Where(e=>e.UserId == userId)
                                .Include(e=>e.OrderItems!)
                                .ThenInclude(e=>e.Product)
                                .AsQueryable();

            return await orders.OrderByDescending(i=>i.OrderDate).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetOrdersCount(string userId)
        {
            return await Context!.Orders
                                .Where(e=>e.UserId == userId)
                                .CountAsync();
        }
    }
}