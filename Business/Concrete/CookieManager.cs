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