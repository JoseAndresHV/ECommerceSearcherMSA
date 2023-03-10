namespace ECommerce.Api.Products.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}
