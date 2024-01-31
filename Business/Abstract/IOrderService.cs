using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task CreateAsync(OrderModel model,string userId);
        Task<OrderListViewModel> GetOrders(string userId, int page);
        Task<OrderListViewModel> GetAllOrders(string orderState,int page);
    }
}