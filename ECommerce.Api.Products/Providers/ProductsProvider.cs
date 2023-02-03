using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers;

public class ProductsProvider : IProductsProvider
{
    private readonly ProductsDbContext _context;
    private readonly ILogger<ProductsProvider> _logger;
    private readonly IMapper _mapper;

    public ProductsProvider(ProductsDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;

        SeedData();
    }

    private void SeedData()
    {
        if (!_context.Products.Any())
        {
            _context.Products.AddRange(
                new ProductEntity { Id = 1, Name = "Keyboard", Price = 20, Inventory = 5 },
                new ProductEntity { Id = 2, Name = "Mouse", Price = 5, Inventory = 50 },
                new ProductEntity { Id = 3, Name = "Monitor", Price = 150, Inventory = 15 },
                new ProductEntity { Id = 4, Name = "CPU", Price = 200, Inventory = 10 }
            );

            _context.SaveChanges();
        }
    }

    public async Task<(bool isSuccess, IEnumerable<ProductModel>? products, string? errorMessage)> GetProductsAsync()
    {
        try
        {
            var products = await _context.Products.ToListAsync();
            if (products is not null && products.Any())
            {
                var result = _mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductModel>>(products);
                return (true, result, null);
            }
            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool isSuccess, ProductModel? products, string? errorMessage)> GetProductAsync(int id)
    {
        try
        {
            var products = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (products is not null)
            {
                var result = _mapper.Map<ProductEntity, ProductModel>(products);
                return (true, result, null);
            }
            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
