using Demo.Talabat.Core;
using Demo.Talabat.Core.Entities;
using Demo.Talabat.Core.Entities.Order_Aggregate;
using Demo.Talabat.Core.Entities.Product;
using Demo.Talabat.Core.Repositories.Contract;
using Demo.Talabat.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        //private Dictionary<string, GenericRepository<BaseEntity>> repositories;
        private Hashtable repositories;
        //DbContext is the class responsible for dealing with database
        //UnitOfWork is the class responsible for dealing with database through the DbContext
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new /*Dictionary<string, GenericRepository<BaseEntity>>*/ Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (!repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(dbContext)/* as GenericRepository<BaseEntity>*/;
                repositories.Add(key, repository);
            }
            return repositories[key] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
               => await dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync()
              => await dbContext.DisposeAsync();


    }
}
