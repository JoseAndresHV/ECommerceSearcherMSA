using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services;

public class OrdersService : IOrdersService
{
    private readonly IHttpClientFactory _http;
    private readonly ILogger<OrdersService> _logger;

    public OrdersService(IHttpClientFactory http, ILogger<OrdersService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<(bool isSuccess, IEnumerable<OrderModel>? orders, string? errorMessage)> GetOrderAsync(int customerId)
    {
        try
        {
            var client = _http.CreateClient("OrdersService");
            var response = await client.GetAsync($"api/orders/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<OrderModel>>(content, options);

                return (true, result, null);
            }

            return (false, null, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
