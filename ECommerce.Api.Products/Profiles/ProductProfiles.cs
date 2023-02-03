using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Profiles;

public class ProductProfiles : Profile
{
	public ProductProfiles()
	{
		CreateMap<ProductEntity, ProductModel>();
	}
}
