using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.ViewModels
{
    public class OrderViewModel
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? OrderState { get; set; }
        public string? PaymentType { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; } = [];
        public List<SelectListItem>? OrderStates { get; set; }

        public double TotalPrice()
        {
            return OrderItems!.Sum(i => i.Price * i.Quantity);
        }
    }
}