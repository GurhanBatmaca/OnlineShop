using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IOrderStateService
    {
        Task CreateAsync(OrderState orderState);
        Task<List<OrderState>> GetAllAsync();
    }
}