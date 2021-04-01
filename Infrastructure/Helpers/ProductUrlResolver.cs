using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return ConfigurationAccessUtility.ApiUrl + source.PictureUrl;
            }

            return null;
        }
    }
}
