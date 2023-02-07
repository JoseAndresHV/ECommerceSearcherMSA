namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool isSuccess, dynamic? customer, string? errorMessage)> GetProductsAsync(int id);
    }
}
