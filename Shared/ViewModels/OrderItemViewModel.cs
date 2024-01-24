namespace Shared.ViewModels
{
    public class OrderItemViewModel
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string? Url { get; set; }

        public double TotalPrice()
        {
            return Price * Quantity;
        }
    }
}