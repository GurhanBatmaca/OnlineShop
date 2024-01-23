using Business.Abstract;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extentions;
using Presentation.Identity.Abstract;
using Presentation.PaymentProcess;
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
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        public OrderController(IHttpContextAccessor? accessor,ISignService signService,IUserService userService,ICartService? cartService,SessionManager? sessionManager,IProductService? productService,IConfiguration configuration,IOrderService orderService)
        {
            _accessor = accessor;
            _signService = signService;
            _userService = userService;
            _cartService = cartService;
            _sessionManager = sessionManager;
            _productService = productService;
            _configuration = configuration;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int sayfa=1)
        {
            return View();
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

                var model = new OrderModel
                {
                    CartModel = new CartViewModel
                    {
                        Id = cart!.Id,
                        CartItems = cart.CartItems!.Select(e => new CartItemViewModel{
                            CartItemId = e.CartItemId,
                            ProductId = e.ProductId,
                            Name = e!.Name,
                            Price = (double)e.Price!,
                            ImageUrl = e.ImageUrl,                           
                            Quantity = e.Quantity
                        }).ToList()
                    }
                };


                return View(model);
            }
        }

        [HttpPost]       
        public async Task<IActionResult> Checkout(OrderModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model);
            };

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

                model.CartModel = new CartViewModel
                {
                    Id = cart!.Id,
                    CartItems = cart.CartItems!.Select(e => new CartItemViewModel{
                        CartItemId = e.CartItemId,
                        ProductId = e.ProductId,
                        Name = e!.Name,
                        Price = (double)e.Price!,
                        ImageUrl = e.ImageUrl,                           
                        Quantity = e.Quantity
                    }).ToList()
                    
                };

                var payment = Process_Iyzipay.Pay(model,_configuration);

                if(payment.Status == "success")
                {
                    model.ConversationId = payment.ConversationId;
                    model.PaymentId = payment.PaymentId;

                    await _orderService.CreateAsync(model,userId!);

                    await _cartService.ClearCartAsync(userId!);

                    TempData.Put("infoMessage",new MessageModel
                    {
                        Title = $"Ödeme başarılı",
                        Message = $"Siparişinizi siparişlerim sayfasından takip edebilirsiniz",
                        AlertType = "success"
                    });

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.Put("infoMessage",new MessageModel
                    {
                        Title = $"Hata",
                        Message = $"Ödeme başarısız: {payment.ErrorMessage}.",
                        AlertType = "danger"
                    });

                    return View(model);
                }
                               
            }
        }
    
        
    
    }
}