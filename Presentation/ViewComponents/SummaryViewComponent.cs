using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Presentation.Identity.Abstract;

namespace Presentation.ViewComponents
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly IHttpContextAccessor? _accessor;
        private readonly IUserService? _userService;
        private readonly ICartService? _cartService;
        public SummaryViewComponent(IHttpContextAccessor? accessor,IUserService userService,ICartService? cartService)
        {
            _accessor = accessor;
            _userService = userService;
            _cartService = cartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                
            }

            var userId = _userService!.GetUserId(_accessor!.HttpContext!);
            var cart = await _cartService!.GetCartByUserId(userId!);
            return View(cart);
        }
    }
}