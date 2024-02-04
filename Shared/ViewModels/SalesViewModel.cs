using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.ViewModels
{
    public class SalesViewModel
    {
        public DateTime OrderDate { get; set; }
        public string? OrderState { get; set; }
        public string? PaymentType { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; } = [];

        public double TotalPrice()
        {
            return OrderItems!.Sum(i => i.Price * i.Quantity);
        }
    }
}