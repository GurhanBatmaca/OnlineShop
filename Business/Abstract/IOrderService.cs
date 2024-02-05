using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IOrderService
    {
        string? Message { get; set; }
        Task CreateAsync(OrderModel model,string userId);
        Task<OrderListViewModel> GetOrders(string userId, int page);
        Task<OrderListViewModel> GetAllOrders(string orderState,int page);
        Task<Order?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(string orderState,int orderId);
        Task<List<TopOrdersViewModel>?> GetTop10Orders();
        Task<OrderTotalViewModel?> GetOrdersTotal();
    }
}