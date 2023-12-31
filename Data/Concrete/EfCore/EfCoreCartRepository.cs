using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Concrete.EfCore
{
    public class EfCoreCartRepository: EfCoreGenericRepository<Cart>,ICartRepository
    {
        public EfCoreCartRepository(ShopContext context):base(context)
        {
            
        }
        protected ShopContext? Context => _context as ShopContext;

        public async Task<Cart?> GetCartByUserId(string userId)
        {
            return await Context!.Carts.Where(e=>e.UserId == userId)
                                        .Include(e=>e.CartItems!)
                                        .ThenInclude(e=>e.Product)
                                        .FirstOrDefaultAsync();
        }

    }
}