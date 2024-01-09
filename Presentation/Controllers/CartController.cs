using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;
using Shared.Models;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController: Controller
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly ISignService? _signService;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        private readonly ICookieService? _cookieService;
        private readonly IProductService? _productService;
        public CartController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService,ICartService? cartService,ICookieService? cookieService,IProductService? productService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
            _cookieService = cookieService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var nonLoggedInUserCart = _cookieService!.GetCart
                (
                    new CookieModel{HttpContext=_accessor!.HttpContext}
                );
                
                return View(nonLoggedInUserCart);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var loggedInUserCart = await _cartService!.GetCartByUserId(userId!);

                return View(loggedInUserCart);
            }           
        }

        public async Task<IActionResult> AddToCart(int productId,int quantity)
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var product = await _productService!.GetProductById(productId);
                _cookieService!.AddToCart(new CookieModel{HttpContext=_accessor!.HttpContext},product!,quantity);
                
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
            var userId = _userService!.GetUserId(_accessor!.HttpContext!);
            var quantity = await _cartService!.IncreaseCartItemQuantity(userId!,productId);
            var cart = await _cartService!.GetCartByUserId(userId!);
            var cartTotalPrice = cart!.TotalPrice();
            var itemPrice = cart?.CartItems?.Find(e=>e.ProductId == productId)!.Price;

            return new JsonResult(new {quantity=quantity,itemPrice=itemPrice,cartTotalPrice=cartTotalPrice});
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