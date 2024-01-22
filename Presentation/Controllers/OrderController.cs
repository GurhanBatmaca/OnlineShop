using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Extentions;
using Presentation.Identity.Abstract;
using Presentation.Session;
using Shared.Models;
using Shared.ViewModels;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class OrderController: Controller
    {
        private readonly  IHttpContextAccessor? _accessor;
        private readonly ISignService? _signService;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        private readonly SessionManager? _sessionManager;
        private readonly IProductService? _productService;
        public OrderController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService,ICartService? cartService,SessionManager? sessionManager,IProductService? productService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
            _sessionManager = sessionManager;
            _productService = productService;
        }

        [HttpGet]       
        public async Task<IActionResult> Checkout()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var sessionCart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );

                var sessionModel = new OrderModel
                {
                    CartModel = new CartViewModel
                    {
                        Id = sessionCart!.Id,
                        CartItems = sessionCart.CartItems!.Select(e => new CartItemViewModel{
                            CartItemId = e.CartItemId,
                            ProductId = e.ProductId,
                            Name = e!.Name,
                            Price = (double)e.Price!,
                            ImageUrl = e.ImageUrl,                           
                            Quantity = e.Quantity
                        }).ToList()
                    }
                };


                return View(sessionModel);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var cart = await _cartService!.GetCartByUserId(userId!);


                return View();
            }
        }

        [HttpPost]       
        public async Task<IActionResult> Checkout(OrderModel model)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var sessionCart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );

                model.CartModel = new CartViewModel
                    {
                        Id = sessionCart!.Id,
                        CartItems = sessionCart.CartItems!.Select(e => new CartItemViewModel{
                            CartItemId = e.CartItemId,
                            ProductId = e.ProductId,
                            Name = e!.Name,
                            Price = (double)e.Price!,
                            ImageUrl = e.ImageUrl,                           
                            Quantity = e.Quantity
                        }).ToList()
                    
                };


                return View(model);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var cart = await _cartService!.GetCartByUserId(userId!);


                return View();
            }
        }
    }
}