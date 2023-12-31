namespace Shared.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItemViewModel>? CartItems { get; set; } = [];
        public double TotalPrice()
        {
            return CartItems!.Sum(i => i.Price * i.Quantity);
        }
        public int TotalItem()
        {
            return CartItems!.Count;
        }
    }
}