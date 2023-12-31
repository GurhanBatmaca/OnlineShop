using Entity;

namespace Data.Abstract
{
    public interface ICartRepository: IRepository<Cart>
    {
        Task<Cart?> GetCartByUserId(string userId);
    }
}