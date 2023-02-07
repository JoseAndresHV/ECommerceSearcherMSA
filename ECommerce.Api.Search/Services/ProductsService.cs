using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services;

public class ProductsService : IProductsService
{
    private readonly IHttpClientFactory _http;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IHttpClientFactory http, ILogger<ProductsService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<(bool isSuccess, IEnumerable<ProductModel>? products, string? errorMessage)> GetProductsAsync()
    {
        try
        {
            var client = _http.CreateClient("ProductsService");
            var response = await client.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(content, option);

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
