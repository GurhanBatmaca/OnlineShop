using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Data.Abstract
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>?> GetOrders(string userId,int page,int pageSize);
        Task<int> GetOrdersCount(string userId);
        Task<List<Order>?> GetAllOrders(string orderState,int page,int pageSize);
        Task<int> GetAllOrdersCount(string orderState);
        Task<List<TopOrdersViewModel>?> GetTop10Orders();
        Task<OrderTotalViewModel?> GetOrdersTotal();
        Task<List<Order>?> GetOrdersBrief();
    }
}