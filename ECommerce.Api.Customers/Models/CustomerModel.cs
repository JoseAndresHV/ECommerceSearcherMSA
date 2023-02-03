namespace ECommerce.Api.Customers.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}
