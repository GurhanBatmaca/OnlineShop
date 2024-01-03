using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController: Controller
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly ISignService? _signService;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        public CartController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService,ICartService? cartService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userService!.GetUserId(_accessor!.HttpContext!);
            var cart = await _cartService!.GetCartByUserId(userId!);
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId,int quantity)
        {
            var userId = _userService!.GetUserId(_accessor!.HttpContext!);
            await _cartService!.AddToCartAsync(userId!,productId,quantity);
            return RedirectToAction("Index");
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
    }

}