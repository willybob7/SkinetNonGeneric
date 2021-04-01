using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts() => Ok(await _repo.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProduct(int id) => Ok(await _repo.GetProductByIdAsync(id));

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() => Ok(await _repo.GetProductBrandsAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() => Ok(await _repo.GetProductTypesAsync());
    }
}
