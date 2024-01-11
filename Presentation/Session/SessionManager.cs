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

        public async Task<CartViewModel?> GetCart(SessionModel model)
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
    }
}