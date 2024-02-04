using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared.ViewModels;

namespace Data.Concrete.EfCore
{
    public class EfCoreOrderRepository: EfCoreGenericRepository<Order>,IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context):base(context)
        {            
        }

        private ShopContext? Context => _context as ShopContext;

        public async Task<List<Order>?> GetAllOrders(string orderState,int page, int pageSize)
        {
            var orders = Context!.Orders
                                .Include(e=>e.OrderItems!)
                                .ThenInclude(e=>e.Product)
                                .Include(e=>e.OrderState)
                                .AsQueryable();

            if(!string.IsNullOrEmpty(orderState))
            {
                orders = orders.Where(e=> e.OrderState!.Url == orderState);
            }

            return await orders.OrderByDescending(i=>i.OrderDate).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAllOrdersCount(string orderState)
        {
            var orders = Context!.Orders.AsQueryable();

            if(!string.IsNullOrEmpty(orderState))
            {
                orders = orders.Where(e=> e.OrderState!.Url == orderState);
            }

            return await orders.CountAsync();
        }

        public async Task<List<SalesViewModel>?> GetTop10Sales()
        {
            var result = Context!.Database.SqlQuery<SalesViewModel>($"SELECT TOP (10) p.Name, count(p.Id) as Sepet, sum(oi.Quantity) as SatisAdet, sum(oi.Quantity * oi.Price) as SatisTotal FROM [ShopDb].[dbo].[OrderItems] as oi inner join [ShopDb].[dbo].[Products] as p on oi.ProductId = p.Id group by p.Name order by SatisTotal desc");

            return await result.ToListAsync();       
        }

        public async Task<List<Order>?> GetOrders(string userId, int page, int pageSize)
        {
            var orders = Context!.Orders
                                .Where(e=>e.UserId == userId)
                                .Include(e=>e.OrderItems!)
                                .ThenInclude(e=>e.Product)
                                .Include(e=>e.OrderState)
                                .AsQueryable();

            return await orders.OrderByDescending(i=>i.OrderDate).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetOrdersCount(string userId)
        {
            return await Context!.Orders
                                .Where(e=>e.UserId == userId)
                                .CountAsync();
        }

        public double GetSalesTotal()
        {
            var orders =  Context!.Orders
                                .Include(e=>e.OrderItems!)
                                .ThenInclude(e=>e.Product)
                                .Include(e=>e.OrderState)
                                .AsQueryable();

            double total = 0;

            foreach (var order in orders)
            {
               total += order.OrderItems!.Sum(e=>e.Price*e.Quantity);
            }

            return total;

        }

    }
}