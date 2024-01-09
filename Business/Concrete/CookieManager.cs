using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class CookieManager: ICookieService
    {       
        public async void AddToCart(CookieModel model,ProductViewModel productViewModel,int quantity)
        {
            var cart = JsonConvert.DeserializeObject<CartViewModel>(model.HttpContext!.Request.Cookies["CartCookie"]!)!;
           
            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productViewModel.Id);

            if(index < 0)
            {                
                cart.CartItems.Add(new CartItemViewModel{
                    ProductId = productViewModel!.Id,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    Quantity = quantity,
                    ImageUrl = productViewModel.ImageUrl
                });
            }
            else
            {
                cart.CartItems[index].Quantity += quantity;
            }

            string cartString = JsonConvert.SerializeObject(cart);
            model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);
        }

        public CartViewModel GetCart(CookieModel model)
        {
            var cart = new CartViewModel();

            if(model.HttpContext!.Request.Cookies["CartCookie"] == null)
            {
                string cartString = JsonConvert.SerializeObject(cart);

                model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);

            }
            else
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(model.HttpContext!.Request.Cookies["CartCookie"]!)!;
            }

            return cart;
        }

    }
}