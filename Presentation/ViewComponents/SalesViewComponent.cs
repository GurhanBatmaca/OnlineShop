using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;

namespace Presentation.ViewComponents
{
    public class SalesViewComponent: ViewComponent
    {
        private readonly IOrderService _orderService;
        public SalesViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Total = await _orderService.GetOrdersTotal();
            var model = await _orderService.GetTop10Orders();

            return View(model);
        }
    }
}