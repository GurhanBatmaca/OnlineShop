using Entity;
using Shared.Models;

namespace Data.Abstract
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>?> GetOrders(string userId,int page,int pageSize);
        Task<int> GetOrdersCount(string userId);
    }
}