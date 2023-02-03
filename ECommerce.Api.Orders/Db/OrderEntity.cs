namespace ECommerce.Api.Orders.Db;

public class OrderEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal Total { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; } = default!;
}
