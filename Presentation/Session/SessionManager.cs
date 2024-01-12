using Business.Abstract;
using Newtonsoft.Json;
using Shared.Models;
using Shared.ViewModels;

namespace Presentation.Session
{
    public class SessionManager
    {
        private readonly IProductService? _productService;
        public SessionManager(IProductService? productService)
        {
            _productService = productService;
        }

        public CartViewModel? GetCart(SessionModel model)
        {
            var cart = new CartViewModel();

            if(model.HttpContext!.Session.GetString("Cart") == null)
            {
                var cartString = JsonConvert.SerializeObject(cart);
                model.HttpContext.Session.SetString("Cart",cartString);
            }
            else
            {
                var cartString = model.HttpContext.Session.GetString("Cart");
                cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);
            }

            return cart;
        }
    
        public async Task AddToCart(SessionModel model,int productId,int quantity)
        {
            var cartString = model.HttpContext!.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);
         
            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);

            if(index < 0)
            {  

                var product = await _productService!.GetProductById(productId);

                cart.CartItems.Add(new CartItemViewModel{
                    ProductId = product!.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl
                });
            }
            else
            {
                cart.CartItems[index].Quantity += quantity;
            }

            cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext.Session.SetString("Cart",cartString);
        }

        public void DeleteFromCart(SessionModel model,int productId)
        {
            var cartString = model.HttpContext!.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);

            var index = cart!.CartItems!.FindIndex(e=>e.ProductId == productId);
            cart.CartItems.Remove(cart.CartItems[index]);

            cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext.Session.SetString("Cart",cartString);
        }
    
        public int IncreaseCartItemQuantity(SessionModel model,int productId)
        {
            var cartString = model.HttpContext!.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);
            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);
            cart.CartItems[index].Quantity += 1;

            cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext.Session.SetString("Cart",cartString);

            return cart.CartItems[index].Quantity;
        }

        public int DecreaseCartItemQuantity(SessionModel model,int productId)
        {
            var cartString = model.HttpContext!.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);
            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);
            cart.CartItems[index].Quantity -= 1;

            cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext.Session.SetString("Cart",cartString);

            return cart.CartItems[index].Quantity;
        }
    
    }
}