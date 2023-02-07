using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Test;

public class ProductsServiceTest
{
    [Fact]
    public async Task GetProductsReturnsAllProducts()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
            .Options;
        var context = new ProductsDbContext(options);
        CreateProducts(context);

        var productProfile = new ProductProfiles();
        var configuration = new MapperConfiguration(c => c.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(context, null!, mapper);

        var products = await productsProvider.GetProductsAsync();

        Assert.True(products.isSuccess);
        Assert.True(products.products?.Any());
        Assert.Null(products.errorMessage);
    }

    [Fact]
    public async Task GetProductReturnsProductUsingValidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
            .Options;
        var context = new ProductsDbContext(options);
        CreateProducts(context);

        var productProfile = new ProductProfiles();
        var configuration = new MapperConfiguration(c => c.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(context, null!, mapper);

        var product = await productsProvider.GetProductAsync(1);

        Assert.True(product.isSuccess);
        Assert.NotNull(product.products);
        Assert.True(product.products.Id == 1);
        Assert.Null(product.errorMessage);
    }

    [Fact]
    public async Task GetProductReturnsProductUsingInvalidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
            .Options;
        var context = new ProductsDbContext(options);
        CreateProducts(context);

        var productProfile = new ProductProfiles();
        var configuration = new MapperConfiguration(c => c.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(context, null!, mapper);

        var product = await productsProvider.GetProductAsync(-1);

        Assert.False(product.isSuccess);
        Assert.Null(product.products);
        Assert.NotNull(product.errorMessage);
    }

    private void CreateProducts(ProductsDbContext context)
    {
        for (int i = 1; i <= 10; i++)
        {
            context.Products.Add(new ProductEntity
            {
                Id = i,
                Name = Guid.NewGuid().ToString(),
                Inventory = i + 10,
                Price = (decimal)(i * 3.14)
            });
        }

        context.SaveChanges();
    }
}