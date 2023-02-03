using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers;

public class OrdersProvider : IOrdersProvider
{
    private readonly OrdersDbContext _context;
    private readonly ILogger<OrdersProvider> _logger;
    private readonly IMapper _mapper;

    public OrdersProvider(OrdersDbContext context, ILogger<OrdersProvider> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        SeedData();
    }

    private void SeedData()
    {
        if (!_context.Orders.Any())
        {
            _context.Orders.Add(new OrderEntity()
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                Total = 100
            });
            _context.Orders.Add(new OrderEntity()
            {
                Id = 2,
                CustomerId = 2,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                Total = 10
            });
            _context.OrderItems.Add(new OrderItemEntity()
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 25
            });
            _context.OrderItems.Add(new OrderItemEntity()
            {
                Id = 2,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 50
            });
            _context.OrderItems.Add(new OrderItemEntity()
            {
                Id = 3,
                OrderId = 2,
                ProductId = 3,
                Quantity = 1,
                UnitPrice = 50
            });

            _context.SaveChanges();
        }
    }
    public async Task<(bool isSuccess, IEnumerable<OrderModel>? orders, string? errorMessage)> GetOrdersAsync(int customerId)
    {
        try
        {
            var orders = await _context.Orders
                .Where(x => x.CustomerId == customerId)
                .Include(x => x.OrderItems)
                .ToListAsync();

            if (orders is not null && orders.Any())
            {
                var result = _mapper.Map<IEnumerable<OrderEntity>,
                    IEnumerable<OrderModel>>(orders);
                return (true, result, null);
            }
            return (false, null, "Not Found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
