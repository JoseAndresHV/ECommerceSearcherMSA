using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers;

public class CustomersProvider : ICustomersProvider
{
    private readonly CustomersDbContext _context;
    private readonly ILogger<CustomersProvider> _logger;
    private readonly IMapper _mapper;

    public CustomersProvider(CustomersDbContext context, ILogger<CustomersProvider> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;

        SeedData();
    }

    private void SeedData()
    {
        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(
                new CustomerEntity { Id = 1, Name = "John Doe", Address = "Avenue 1" },
                new CustomerEntity { Id = 2, Name = "Jona Pou", Address = "Avenue 2" },
                new CustomerEntity { Id = 3, Name = "Rick Asin", Address = "Avenue 3" }
            );

            _context.SaveChanges();
        }
    }

    public async Task<(bool isSuccess, IEnumerable<CustomerModel>? customers, string? errorMessage)> GetCustomersAsync()
    {
        try
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers is not null && customers.Any())
            {
                var result = _mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<CustomerModel>>(customers);
                return (true, result, null);
            }
            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool isSuccess, CustomerModel? customer, string? errorMessage)> GetCustomerAsync(int id)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer is not null)
            {
                var result = _mapper.Map<CustomerEntity, CustomerModel>(customer);
                return (true, result, null);
            }
            return (false, null, "Not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
