using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();

        ///public IGenericRepository<Product> ProductsRepo { get; set; }
        ///public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
        ///public IGenericRepository<ProductCategory> CategoriesRepo { get; set; }
        ///public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
        ///public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        ///public IGenericRepository<Order> OrdersRepo { get; set; }
    }
}
