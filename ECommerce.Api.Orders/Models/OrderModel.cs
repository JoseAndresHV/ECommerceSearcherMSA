namespace ECommerce.Api.Orders.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemModel> OrderItems { get; set; } = default!;
}
