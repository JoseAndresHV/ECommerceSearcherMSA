using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces;

public interface IOrdersProvider
{
    Task<(bool isSuccess, IEnumerable<OrderModel>? orders, string? errorMessage)> GetOrdersAsync(int customerId);
}
