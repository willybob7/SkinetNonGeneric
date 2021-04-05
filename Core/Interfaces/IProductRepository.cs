using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductToReturnDTO> GetProductByIdAsync(int id);
        Task<IReadOnlyList<ProductToReturnDTO>> GetProductsAsync(string sort, int? brandId, int? typeId);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
