using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface ICookieService
    {
        CartViewModel GetCart(CookieModel model);
    }
}