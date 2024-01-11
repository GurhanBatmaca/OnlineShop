using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
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

        protected HttpContext? _HttpContext => _accessor!.HttpContext;

        public async Task<IActionResult> Index()
        {
            var cart = new CartViewModel();
            if(!User.Identity!.IsAuthenticated)
            {
                cart = _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_HttpContext}
                );
                
                return View(cart);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                cart = await _cartService!.GetCartByUserId(userId!);

                return View(cart);
            }           
        }

        public async Task<IActionResult> AddToCart(int productId,int quantity)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                await _sessionManager!.AddToCart(new CookieModel{HttpContext=_accessor!.HttpContext},productId!,quantity);
                return RedirectToAction("Index");
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                await _cartService!.AddToCartAsync(userId!,productId,quantity);
                return RedirectToAction("Index");
            }           
        }

        [HttpPost]
        public async Task<JsonResult> IncreaseCartItem(int productId)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                // var nonLoggedInQuantity = _cookieService!.IncreaseCartItemQuantity(new CookieModel{HttpContext=_accessor!.HttpContext},productId);
                // var nonLoggedInUserCart = await _cookieService!.GetCart
                // (
                //     new CookieModel{HttpContext=_accessor!.HttpContext}
                // );
                // var nonLoggedInUserCartItemPrice = nonLoggedInUserCart.CartItems!.Find(e=>e.ProductId == productId)!.Price;
                // var nonLoggedInUserCartTotalPrice = nonLoggedInUserCart.TotalPrice();

                // return new JsonResult(new {quantity=nonLoggedInQuantity,itemPrice=nonLoggedInUserCartItemPrice,cartTotalPrice=nonLoggedInUserCartTotalPrice});
                return new JsonResult(new {quantity=1,itemPrice=1,cartTotalPrice=1});
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
            var userId = _userService!.GetUserId(_accessor!.HttpContext!);
            var quantity = await _cartService!.DecreaseCartItemQuantity(userId!,productId);
            var cart = await _cartService!.GetCartByUserId(userId!);
            var cartTotalPrice = cart!.TotalPrice();
            var itemPrice = cart?.CartItems?.Find(e=>e.ProductId == productId)!.Price;

            return new JsonResult(new {quantity=quantity,itemPrice=itemPrice,cartTotalPrice=cartTotalPrice});
        }

        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                _sessionManager!.DeleteFromCart(new CookieModel{HttpContext=_HttpContext},productId);   
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