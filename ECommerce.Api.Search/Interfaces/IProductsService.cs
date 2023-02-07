using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces;

public interface IProductsService
{
    Task<(bool isSuccess, IEnumerable<ProductModel>? products, string? errorMessage)> GetProductsAsync();
}
