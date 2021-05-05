﻿using API.Errors;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        // the [FromQuery] tag is needed if the controller method expects an object/class for the input
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductParams @params) 
            => Ok(await _repo.GetProductsAsync(@params));

        [HttpGet("{id}")]
        //these are tags to tell swagger what our responses can be
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);

            if (product is null) 
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() => Ok(await _repo.GetProductBrandsAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() => Ok(await _repo.GetProductTypesAsync());
    }
}
