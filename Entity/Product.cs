﻿namespace Entity;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsApproved { get; set; }
    public bool IsHome { get; set; }
    public int StockQuantity { get; set; }
    public DateTime DateAdded { get; set; }
    
    public List<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();
}
