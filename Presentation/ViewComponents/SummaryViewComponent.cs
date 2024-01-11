using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;
using Presentation.Session;
using Shared.Models;
using Shared.ViewModels;

namespace Presentation.ViewComponents
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        private readonly SessionManager? _sessionManager;

        public SummaryViewComponent(IHttpContextAccessor? accessor,IUserService userService,ICartService? cartService,SessionManager? sessionManager)
        {
            _accessor = accessor;
            _userService = userService;
            _cartService = cartService;
            _sessionManager = sessionManager;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var cart = await _sessionManager!.GetCart
                (
                    new SessionModel{HttpContext=_accessor!.HttpContext}
                );
                
                return View(cart);
            } 
            else
            {
                var userId = _userService!.GetUserId(_accessor!.HttpContext!);
                var loggedInUserCart = await _cartService!.GetCartByUserId(userId!);
                
                return View(loggedInUserCart);
            }          
        }
    }
}