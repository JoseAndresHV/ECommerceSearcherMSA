using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrdersService _ordersService;
    private readonly IProductsService _productsService;

    public SearchService(IOrdersService ordersService, IProductsService productsService)
    {
        _ordersService = ordersService;
        _productsService = productsService;
    }

    public async Task<(bool isSuccess, dynamic? searchResults)> SearchAsync(int customer)
    {
        var ordersResult = await _ordersService.GetOrderAsync(customer);
        var productsResult = await _productsService.GetProductsAsync();

        if (ordersResult.isSuccess)
        {
            foreach (var order in ordersResult.orders!)
            {
                foreach (var item in order.OrderItems)
                {
                    item.ProductName = productsResult.isSuccess ?
                        productsResult.products!.FirstOrDefault(p => p.Id == item.ProductId)!.Name :
                        "Product information is not available";
                }
            }

            var result = new { Orders = ordersResult.orders };

            return (true, result);
        }
        return (false, null);
    }
}
