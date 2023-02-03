using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Db;

public class CustomersDbContext : DbContext
{
    public DbSet<CustomerEntity> Customers { get; set; }

    public CustomersDbContext(DbContextOptions options) : base(options)
    {
    }
}