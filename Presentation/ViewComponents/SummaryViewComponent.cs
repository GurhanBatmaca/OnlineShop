using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;
using Shared.Models;
using Shared.ViewModels;

namespace Presentation.ViewComponents
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        private readonly ICookieService? _cookieService;

        public SummaryViewComponent(IHttpContextAccessor? accessor,IUserService userService,ICartService? cartService,ICookieService? cookieService)
        {
            _accessor = accessor;
            _userService = userService;
            _cartService = cartService;
            _cookieService = cookieService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                var nonLoggedInUserCart = await _cookieService!.GetCart
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
    }
}