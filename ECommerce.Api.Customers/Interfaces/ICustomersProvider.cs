using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces;

public interface ICustomersProvider
{
    Task<(bool isSuccess, IEnumerable<CustomerModel>? customers, string? errorMessage)> GetCustomersAsync();
    Task<(bool isSuccess, CustomerModel? customer, string? errorMessage)> GetCustomerAsync(int id);
}
