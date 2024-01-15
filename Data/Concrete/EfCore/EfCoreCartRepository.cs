using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Data.Concrete.EfCore
{
    public class EfCoreCartRepository: EfCoreGenericRepository<Cart>,ICartRepository
    {
        public EfCoreCartRepository(ShopContext context):base(context)
        {
            
        }
        protected ShopContext? Context => _context as ShopContext;

        public async Task<CartInfoModel?> AddToCartAsync(string userId, int productId, int quantity)
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

            var product = await Context.Products.FirstOrDefaultAsync(e=>e.Id == productId);

            return new CartInfoModel
            {
                ProductName = product!.Name,
                ProductWeight = product.Weight,
                CartUnit = cart.CartItems.Count,
                ProductQuantity = index < 0 ? quantity : cart.CartItems[index].Quantity
            };
        }

        public async Task<int> DecreaseCartItemQuantity(string userId, int productId)
        {
            var cart = await Context!.Carts
                                .Include(e=>e.CartItems!)
                                .ThenInclude(e=>e.Product)
                                .FirstOrDefaultAsync(e=>e.UserId==userId);

            var index = cart!.CartItems!.FindIndex(e=>e.ProductId == productId);

            if(cart.CartItems[index].Quantity > 1)
            {
                cart.CartItems[index].Quantity -= 1;
            }            

            await Context.SaveChangesAsync();

            return cart.CartItems[index].Quantity;
        }

        public async Task DeleteFromCart(string userId, int productId)
        {
            var cart = Context!.Carts.FirstOrDefault(e=>e.UserId == userId);
            var item = Context.CartItems.FirstOrDefault(e=>e.ProductId == productId && e.CartId == cart!.Id);
            Context.CartItems.Remove(item!);
            await Context.SaveChangesAsync();
        }


        public async Task<Cart?> GetCartByUserId(string userId)
        {
            return await Context!.Carts
                                        .Include(e=>e.CartItems!)
                                        .ThenInclude(e=>e.Product)
                                        .FirstOrDefaultAsync(e=>e.UserId == userId);
        }

        public async Task<int> IncreaseCartItemQuantity(string userId, int productId)
        {
            var cart = await Context!.Carts
                                .Include(e=>e.CartItems!)
                                .ThenInclude(e=>e.Product)
                                .FirstOrDefaultAsync(e=>e.UserId==userId);

            var index = cart!.CartItems!.FindIndex(e=>e.ProductId == productId);

            cart.CartItems[index].Quantity += 1;

            await Context.SaveChangesAsync();

            return cart.CartItems[index].Quantity;
        }
    }
}