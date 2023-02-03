namespace ECommerce.Api.Products.Db;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}
