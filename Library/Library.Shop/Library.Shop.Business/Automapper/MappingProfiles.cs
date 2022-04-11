using AutoMapper;
using Library.Shop.Business.CQRS.Contracts.Queries;
using Library.Shop.Domain.Models;

namespace Library.Shop.Business.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Cart, GetCartQueryResult>();
            CreateMap<CartProduct, GetProductCartQueryResult>();
        }
    }
}
