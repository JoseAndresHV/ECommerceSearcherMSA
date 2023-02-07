using ECommerce.Api.Search.Interfaces;
using System.Text.Json;

namespace ECommerce.Api.Search.Services;

public class CustomersServices : ICustomersService
{
    private readonly IHttpClientFactory _http;
    private readonly ILogger<CustomersServices> _logger;

    public CustomersServices(IHttpClientFactory http, ILogger<CustomersServices> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<(bool isSuccess, dynamic? customer, string? errorMessage)> GetProductsAsync(int id)
    {
        try
        {
            var client = _http.CreateClient("CustomersService");
            var response = await client.GetAsync($"api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<dynamic>(content, options);

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
