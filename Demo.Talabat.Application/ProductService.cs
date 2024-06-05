using Demo.Talabat.Core;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Services.Contract;
using Demo.Talabat.Core.Specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Application
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);

            var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<Product?> GetProductAsync(int productId)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productId);
            var product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            return product;
        }
        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var countSpec = new ProductsWithFilterationForCountSpecifications(specParams);
            var count = await unitOfWork.Repository<Product>().GetCountAsync(countSpec);
            return count;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
      =>await unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
            => await unitOfWork.Repository<ProductCategory>().GetAllAsync();


    }
}
