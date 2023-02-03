using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersProvider _ordersProvider;

    public OrdersController(IOrdersProvider ordersProvider)
    {
        _ordersProvider = ordersProvider;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetOrdersAsync(int customerId)
    {
        var result = await _ordersProvider.GetOrdersAsync(customerId);
        if (result.isSuccess)
        {
            return Ok(result.orders);
        }
        return NotFound(result.errorMessage);
    }
}
