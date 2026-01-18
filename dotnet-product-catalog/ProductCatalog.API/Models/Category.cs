namespace ProductCatalog.API.Models;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}