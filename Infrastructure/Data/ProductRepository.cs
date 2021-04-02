using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Infrastructure.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        //not needed if you use dapper
        //private readonly StoreContext _context;

        //public ProductRepository(StoreContext context)
        //{
        //    _context = context;
        //}

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            //return await _context.ProductBrands.ToListAsync();
            using (IDbConnection db = new SqliteConnection(ConfigurationAccessUtility.ConnectionString))
            {
                return db.Query<ProductBrand>("Select * from ProductBrands").ToList();
            }
        }

        public async Task<ProductToReturnDTO> GetProductByIdAsync(int id)
        {
            //return await _context.Products.AsNoTracking()
            //    .Include(p => p.ProductBrand)
            //    .Include(p => p.ProductType)
            //    .FirstOrDefaultAsync(p => p.Id == id);
            //return product;


            using (IDbConnection db = new SqliteConnection(ConfigurationAccessUtility.ConnectionString))
            {
                var product = db.Query<Product>($"Select Id, Name, Description, Cast(Price as real) as Price, " +
                    $"PictureUrl, ProductTypeId, ProductBrandId from Products WHERE Id = {id}").FirstOrDefault();

                if (product is not null)
                {
                    product.ProductBrand = db.Query<ProductBrand>($"Select * from ProductBrands where Id = {product.ProductBrandId}").FirstOrDefault();
                    product.ProductType = db.Query<ProductType>($"Select * from ProductTypes where Id = {product.ProductTypeId}").FirstOrDefault();

                    return MapperWrapper.Mapper.Map<ProductToReturnDTO>(product);
                }
                else return null;



                //var result = new ProductToReturnDTO()
                //{
                //    Id = product.Id,
                //    Name = product.Name,
                //    Description = product.Description,
                //    PictureUrl = product.PictureUrl,
                //    Price = product.Price,
                //    ProductBrand = product.ProductBrand.Name,
                //    ProductType = product.ProductType.Name
                //};

                //return result;
            }
        }

        public async Task<IReadOnlyList<ProductToReturnDTO>> GetProductsAsync()
        {
            //return  await _context.Products.AsNoTracking()
            //    .Include(p => p.ProductBrand)
            //    .Include(p => p.ProductType)
            //    .ToListAsync();

            using (IDbConnection db = new SqliteConnection(ConfigurationAccessUtility.ConnectionString))
            {
                //use the "Cast(<columnName> as real) as <columnName>" workaround for decimals in Sqlite 
                var products = db.Query<Product>($"Select Id, Name, Description, Cast(Price as real) as Price," +
                    $" PictureUrl, ProductTypeId, ProductBrandId from Products").ToList();
                var productBrandIds = products.Select(p => p.ProductBrandId).ToList();
                var productTypeIds = products.Select(p => p.ProductTypeId).ToList();
                var productBrands = db.Query<ProductBrand>("Select * from ProductBrands where Id in @IDs", new {IDs = productBrandIds}).ToList();
                var productTypes = db.Query<ProductType>("Select * from ProductTypes where Id in @IDs", new {IDs = productTypeIds}).ToList();

                foreach (var product in products)
                {
                    product.ProductBrand = productBrands.FirstOrDefault(b => b.Id == product.ProductBrandId);
                    product.ProductType = productTypes.FirstOrDefault(t => t.Id == product.ProductTypeId);
                }

                return products.Select(product => MapperWrapper.Mapper.Map<ProductToReturnDTO>(product)).ToList();

                //first draft
                //foreach(var product in products)
                //{
                //product.ProductBrand = productBrands.FirstOrDefault(b => b.Id == product.ProductBrandId);
                //product.ProductType = productTypes.FirstOrDefault(t => t.Id == product.ProductTypeId);
                //}

                //return products;

                //second draft
                //var result = products.Select(product => new ProductToReturnDTO()
                //{
                //    Id = product.Id,
                //    Name = product.Name,
                //    Description = product.Description,
                //    PictureUrl = product.PictureUrl,
                //    Price = product.Price,
                //    ProductBrand = productBrands.FirstOrDefault(b => b.Id == product.ProductBrandId).Name,
                //    ProductType = productTypes.FirstOrDefault(t => t.Id == product.ProductTypeId).Name
                //}).ToList();

                //return result;
            }
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            //return await _context.ProductTypes.ToListAsync();
            using (IDbConnection db = new SqliteConnection(ConfigurationAccessUtility.ConnectionString))
            {
                return db.Query<ProductType>("Select * from ProductTypes").ToList();
            }
        }
    }
}
