namespace Entity;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }

    public List<ProductCategory>? ProductCategories { get; set; } = [];

}
