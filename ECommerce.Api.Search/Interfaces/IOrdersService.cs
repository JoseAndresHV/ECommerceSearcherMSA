using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces;

public interface IOrdersService
{
    Task<(bool isSuccess, IEnumerable<OrderModel>? orders, string? errorMessage)> GetOrderAsync(int customerId);
}
