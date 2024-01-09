using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface ICookieService
    {
        CartViewModel GetCart(CookieModel model);
        void AddToCart(CookieModel model,ProductViewModel productViewModel,int quantity);
        void DeleteFromCart(CookieModel model,int productId);
        int IncreaseCartItemQuantity(CookieModel model,int productId);
    }
}