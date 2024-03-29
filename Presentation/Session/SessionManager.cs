using System.Text;
using Business.Abstract;
using Microsoft.AspNetCore.WebUtilities;
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
    
        public async Task<CartInfoModel> AddToCart(SessionModel model,int productId,int quantity)
        {
            var cartString = model.HttpContext!.Session.GetString("Cart");
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartString!);
         
            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);
            var product = await _productService!.GetProductById(productId);

            if(index < 0)
            {  
                cart.CartItems.Add(new CartItemViewModel{
                    ProductId = product!.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl,
                    Weight = product.Weight
                });
            }
            else
            {
                cart.CartItems[index].Quantity += quantity;
            }

            cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext.Session.SetString("Cart",cartString);

            return new CartInfoModel
            {
                ProductName = product!.Name,
                ProductWeight = product.Weight,
                CartUnit = cart.TotalItem(),
                ProductQuantity = index < 0 ? quantity : cart.CartItems[index].Quantity
            };
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
    
        public void ClearCart(SessionModel model)
        {
            var cart = new CartViewModel();
            var cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext!.Session.SetString("Cart",cartString);

        }
    
        public string GetUserIdCookie(SessionModel model)
        {

            string userId = string.Empty;

            if(model.HttpContext!.Request.Cookies["GuidId"] == null)
            {

                var cookie = new CookieOptions{Expires = DateTime.Now.AddDays(30)};
                userId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes($"nonMemberUserId_{Guid.NewGuid()}"));

                model.HttpContext!.Response.Cookies.Append("GuidId", userId, cookie);
                
            }
            else
            {
                userId = model.HttpContext!.Request.Cookies["GuidId"]!;
            }
           
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(userId));
        }
    
    }
}