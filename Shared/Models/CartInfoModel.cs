namespace Shared.Models
{
    public class CartInfoModel
    {
        public string? ProductName { get; set; }
        public double ProductWeight { get; set; }
        public int ProductQuantity { get; set; }
        public int CartUnit { get; set; }
    }
}