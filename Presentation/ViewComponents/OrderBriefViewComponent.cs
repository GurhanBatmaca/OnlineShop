using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;

namespace Presentation.ViewComponents
{
    public class OrderBriefViewComponent: ViewComponent
    {
        private readonly IOrderService _orderService;
        public OrderBriefViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _orderService.GetOrdersBrief();

            return View(model);
        }
    }
}