using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface ICartService
    {
        Task CreateAsync(Cart cart);
        Task<CartViewModel?> GetCartByUserId(string userId);
        Task AddToCartAsync(string userId,int productId,int quantity);
        Task<int> IncreaseCartItemQuantity(string userId,int productId);
        Task<int> DecreaseCartItemQuantity(string userId,int productId);
    }
}