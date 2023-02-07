using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrdersService _ordersService;
    private readonly IProductsService _productsService;
    private readonly ICustomersService _customersService;

    public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
    {
        _ordersService = ordersService;
        _productsService = productsService;
        _customersService = customersService;
    }

    public async Task<(bool isSuccess, dynamic? searchResults)> SearchAsync(int customer)
    {
        var ordersResult = await _ordersService.GetOrderAsync(customer);
        var productsResult = await _productsService.GetProductsAsync();
        var customerResult = await _customersService.GetProductsAsync(customer);

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

            var result = new
            {
                Orders = ordersResult.orders,
                Customer = customerResult.isSuccess ?
                    customerResult.customer :
                    new { Name = "Customer information is not available" }
            };

            return (true, result);
        }
        return (false, null);
    }
}
