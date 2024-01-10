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

        private readonly IProductService? _productService;
        public CookieManager(IProductService? productService)
        {
            _productService = productService;
        }   
        // public async void AddToCart(CookieModel model,ProductViewModel productViewModel,int quantity)
        // {
        //     var pro = await _productService!.GetProductById(1);
        //     var cartCookie = model.HttpContext!.Request.Cookies["CartCookie"];
        //     var cart = JsonConvert.DeserializeObject<CartViewModel>(cartCookie!)!;
           
        //     var index = cart!.CartItems!.FindIndex(i => i.ProductId == productViewModel.Id);

        //     if(index < 0)
        //     {                
        //         cart.CartItems.Add(new CartItemViewModel{
        //             ProductId = productViewModel!.Id,
        //             // Name = productViewModel.Name,
        //             // Price = productViewModel.Price,
        //             Quantity = quantity,
        //             // ImageUrl = productViewModel.ImageUrl
        //         });
        //     }
        //     else
        //     {
        //         cart.CartItems[index].Quantity += quantity;
        //     }

        //     string cartString = JsonConvert.SerializeObject(cart);
        //     model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);
        // }

        // public void DeleteFromCart(CookieModel model, int productId)
        // {
        //     var cartCookie = model.HttpContext!.Request.Cookies["CartCookie"];
        //     var cart = JsonConvert.DeserializeObject<CartViewModel>(cartCookie!)!;
           
        //     var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);

        //     cart.CartItems.Remove(cart.CartItems[index]);
        //     string cartString = JsonConvert.SerializeObject(cart);
        //     model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);
        // }


        public async Task<CartViewModel> GetCart(CookieModel model)
        {
            var cart = new CartViewModel();

            if(model.HttpContext!.Request.Cookies["CartCookie"] == null)
            {
                string cartString = JsonConvert.SerializeObject(cart);

                model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);

            }
            else
            {
                var cartCookie = model.HttpContext!.Request.Cookies["CartCookie"];
                cart = JsonConvert.DeserializeObject<CartViewModel>(cartCookie!)!;

                var ids = cart.CartItems!.Select(e => e.ProductId);
                var productListModel = new ProductListViewModel();
                productListModel = await _productService!.GetProductListByIds(ids);
                productListModel.Products = [];

                foreach (var item in cart.CartItems!)
                {
                    foreach (var product in productListModel.Products)
                    {
                        if(item.ProductId == product.Id) 
                        {
                            item.Price = product.Price;
                            item.Name = product.Name;
                            item.ImageUrl = product.ImageUrl;
                        }
                    }
                }
            }

            
            return cart;
        }

        // public int IncreaseCartItemQuantity(CookieModel model,int productId)
        // {
        //     var cartCookie = model.HttpContext!.Request.Cookies["CartCookie"];
        //     var cart = JsonConvert.DeserializeObject<CartViewModel>(cartCookie!)!;

        //     var index = cart!.CartItems!.FindIndex(e=>e.ProductId == productId);
        //     cart.CartItems[index].Quantity += 1;

        //     string cartString = JsonConvert.SerializeObject(cart);
        //     model.HttpContext!.Response.Cookies.Append("CartCookie", cartString, model.CookieOptions!);

        //     return cart.CartItems[index].Quantity;
        // }
    
    
    }
}