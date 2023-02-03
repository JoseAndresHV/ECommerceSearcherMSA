using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderItemEntity, OrderItemModel>();
        CreateMap<OrderEntity, OrderModel>()
            .ForMember(x => x.OrderItems, opt => opt.MapFrom(x => x.OrderItems));
    }
}
