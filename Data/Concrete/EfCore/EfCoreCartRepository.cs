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

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cart = await Context!.Carts
                                .Include(e=>e.CartItems!)
                                .ThenInclude(e=>e.Product)
                                .FirstOrDefaultAsync(e=>e.UserId==userId);

            var index = cart!.CartItems!.FindIndex(e=>e.ProductId == productId);

            if(index < 0)
            {
                cart.CartItems.Add(new CartItem{
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id
                });
            }
            else
            {
                cart.CartItems[index].Quantity += quantity;
            }

            await Context.SaveChangesAsync();
        }


        public async Task<Cart?> GetCartByUserId(string userId)
        {
            return await Context!.Carts
                                        .Include(e=>e.CartItems!)
                                        .ThenInclude(e=>e.Product)
                                        .FirstOrDefaultAsync(e=>e.UserId == userId);
        }

    }
}