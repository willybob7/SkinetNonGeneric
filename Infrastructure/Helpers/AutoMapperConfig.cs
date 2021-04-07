using Core.DTOs;
using Core.Entities;

namespace Infrastructure.Helpers
{

    //dependency injection approach
    //class MappingProfiles : Profile
    //{
    //    public MappingProfiles()
    //    {
    //        CreateMap<Product, ProductToReturnDTO>();
    //    }
    //}

    //for using automapper without dependency injection
    //public static class AutoMapperConfig
    //{
    //    public static void Configure()
    //    {
    //        MapperWrapper.Initialize(cfg =>
    //        {
    //            cfg.CreateMap<Product, ProductToReturnDTO>()
    //                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
    //                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
    //                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
    //        });

    //        MapperWrapper.AssertConfigurationIsValid();
    //    }
    //}
}
