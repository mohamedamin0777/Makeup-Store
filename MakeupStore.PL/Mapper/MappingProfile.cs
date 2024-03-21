using AutoMapper;
using MakeupStore.DAL.Entities;
using MakeupStore.PL.Models;

namespace MakeupStore.PL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ProductCategoryViewModel, ProductCategory>().ReverseMap();
            CreateMap<BrandViewModel, ProductBrand>().ReverseMap();
            CreateMap<OrderViewModel, Order>().ReverseMap();
            CreateMap<OrderDetailsViewModel, Order>().ReverseMap();
        }
    }
}
