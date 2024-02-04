using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

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
            ViewBag.Total = _orderService.GetSalesTotal();

            var model = await _orderService.GetAllOrdersForSales();

            return View(model);
        }
    }
}