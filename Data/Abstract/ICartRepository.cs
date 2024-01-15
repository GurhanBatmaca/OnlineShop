using Entity;
using Shared.Models;

namespace Data.Abstract
{
    public interface ICartRepository: IRepository<Cart>
    {
        Task<Cart?> GetCartByUserId(string userId);
        Task<CartInfoModel?> AddToCartAsync(string userId,int productId,int quantity);
        Task<int> IncreaseCartItemQuantity(string userId,int productId);
        Task<int> DecreaseCartItemQuantity(string userId,int productId);
        Task DeleteFromCart(string userId,int productId);
    }
}