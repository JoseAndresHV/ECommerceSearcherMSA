using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomersProvider _customersProvider;

    public CustomersController(ICustomersProvider productsProvider)
    {
        _customersProvider = productsProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomersAsync()
    {
        var result = await _customersProvider.GetCustomersAsync();
        if (result.isSuccess)
        {
            return Ok(result.customers);
        }
        return NotFound(result.errorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerAsync(int id)
    {
        var result = await _customersProvider.GetCustomerAsync(id);
        if (result.isSuccess)
        {
            return Ok(result.customer);
        }
        return NotFound(result.errorMessage);
    }
}
