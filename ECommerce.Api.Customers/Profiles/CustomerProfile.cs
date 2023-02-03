using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Profiles;

public class CustomerProfile : Profile
{
	public CustomerProfile()
	{
		CreateMap<CustomerEntity, CustomerModel>();
	}
}
