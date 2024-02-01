namespace Entity;

public class OrderState
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    
    public List<Order>? Orders { get; set; } = [];

}
