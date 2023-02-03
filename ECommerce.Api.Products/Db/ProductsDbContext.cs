using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Db;

public class ProductsDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }
}
