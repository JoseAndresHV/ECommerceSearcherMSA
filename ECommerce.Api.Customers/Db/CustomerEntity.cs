namespace ECommerce.Api.Customers.Db;

public class CustomerEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}
