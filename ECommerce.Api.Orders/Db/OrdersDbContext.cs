using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Db;

public class OrdersDbContext : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }

    public OrdersDbContext(DbContextOptions options) : base(options)
    {
    }
}
