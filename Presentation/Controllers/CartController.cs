using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Identity.Abstract;
using Presentation.Session;
using Shared.Models;
using Shared.ViewModels;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController: Controller
    {
        private readonly  IHttpContextAccessor? _accessor;
        private readonly ISignService? _signService;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        private readonly SessionManager? _sessionManager;
        private readonly IProductService? _productService;

        public CartController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService,ICartService? cartService,SessionManager? sessionManager,IProductService? productService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
            _sessionManager = sessionManager;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var sessionCart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );
                
                return View(sessionCart);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var cart = await _cartService!.GetCartByUserId(userId!);

                return View(cart);
            }           
        }

        [HttpPost]
        public async Task<JsonResult> AddToCart(int productId,int quantity)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var result = await _sessionManager!.AddToCart(new SessionModel{HttpContext=_accessor!.HttpContext},productId!,quantity);
                return new JsonResult(result);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var result = await _cartService!.AddToCartAsync(userId!,productId,quantity);
                return new JsonResult(result);
            }           
        }

        [HttpPost]
        public async Task<JsonResult> IncreaseCartItem(int productId)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var sessionQuantity = _sessionManager!.IncreaseCartItemQuantity(new SessionModel{HttpContext=_accessor!.HttpContext},productId);

                var sessionCart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );

                var sessionItemPrice = sessionCart!.CartItems!.Find(e=>e.ProductId == productId)!.Price;
                var sessionTotalPrice = sessionCart.TotalPrice();

                return new JsonResult(new {quantity=sessionQuantity,itemPrice=sessionItemPrice,cartTotalPrice=sessionTotalPrice});
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var quantity = await _cartService!.IncreaseCartItemQuantity(userId!,productId);
                var cart = await _cartService!.GetCartByUserId(userId!);
                var cartTotalPrice = cart!.TotalPrice();
                var itemPrice = cart?.CartItems?.Find(e=>e.ProductId == productId)!.Price;

                return new JsonResult(new {quantity=quantity,itemPrice=itemPrice,cartTotalPrice=cartTotalPrice});
            }  
            
        }
        [HttpPost]
        public async Task<JsonResult> DecreaseCartItem(int productId)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var sessionQuantity = _sessionManager!.DecreaseCartItemQuantity(new SessionModel{HttpContext=_accessor!.HttpContext},productId);

                var sessionCart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );

                var sessionItemPrice = sessionCart!.CartItems!.Find(e=>e.ProductId == productId)!.Price;
                var sessionTotalPrice = sessionCart.TotalPrice();

                return new JsonResult(new {quantity=sessionQuantity,itemPrice=sessionItemPrice,cartTotalPrice=sessionTotalPrice});
            }
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var quantity = await _cartService!.DecreaseCartItemQuantity(userId!,productId);
                var cart = await _cartService!.GetCartByUserId(userId!);
                var cartTotalPrice = cart!.TotalPrice();
                var itemPrice = cart?.CartItems?.Find(e=>e.ProductId == productId)!.Price;

                return new JsonResult(new {quantity=quantity,itemPrice=itemPrice,cartTotalPrice=cartTotalPrice});
            }
            
        }

        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                _sessionManager!.DeleteFromCart(new SessionModel{HttpContext=_accessor!.HttpContext},productId);   
                return RedirectToAction("Index");
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                await _cartService!.DeleteFromCart(userId!,productId);
                return RedirectToAction("Index");
            }  
            
        }
    }

}