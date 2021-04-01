using AutoMapper;
using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    //class MappingProfiles : Profile
    //{
    //    public MappingProfiles()
    //    {
    //        CreateMap<Product, ProductToReturnDTO>();
    //    }
    //}
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            MapperWrapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductToReturnDTO>()
                    .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                    .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                    .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            });

            MapperWrapper.AssertConfigurationIsValid();
        }
    }
}
