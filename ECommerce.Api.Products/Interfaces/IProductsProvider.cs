using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces;

public interface IProductsProvider
{
    Task<(bool isSuccess, IEnumerable<ProductModel>? products, string? errorMessage)> GetProductsAsync();
    Task<(bool isSuccess, ProductModel? products, string? errorMessage)> GetProductAsync(int id);
}
